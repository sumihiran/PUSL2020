using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PUSL2020.Application;
using PUSL2020.Application.Data;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Application.Services;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;
using PUSL2020.Tests.Shared;

namespace PUSL2020.Web.Data;

[Order(200)]
public class DemoAccidentsSeeder : IApplicationInitializer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IApplicationDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DemoAccidentsSeeder(IServiceProvider serviceProvider, IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _serviceProvider = serviceProvider;
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task InitialiseAsync()
    {
        var faker = new Faker();
        var employeeUserManager = _serviceProvider.GetRequiredService<UserManager<EmployeeUser>>();

        var rda = await _dbContext.RdaOffices.FirstAsync(f => f.District == District.Colombo);
        var rdaOfficer = FakeModel.RdaEmployee.Generate();
        rdaOfficer.Office = rda;
        await employeeUserManager.CreateAsync(new EmployeeUser(rdaOfficer), faker.Internet.Password());

        var station = await _dbContext.PoliceStations.FirstAsync(p =>
            p.Division.Contains("Colombo", StringComparison.CurrentCultureIgnoreCase));
        var policeOfficer = FakeModel.PoliceOfficer.Generate();
        policeOfficer.Station = station;

        await employeeUserManager.CreateAsync(new EmployeeUser(policeOfficer), faker.Internet.Password());

        var aia = await _dbContext.Insurances.FirstAsync(i =>
            i.Name.Contains("AIA", StringComparison.CurrentCultureIgnoreCase));
        var insuranceEmployee = FakeModel.InsuranceEmployee.Generate();
        insuranceEmployee.HeadOffice = aia;

        await employeeUserManager.CreateAsync(new EmployeeUser(insuranceEmployee), faker.Internet.Password());

        var userManager = _serviceProvider.GetRequiredService<UserManager<ReporterUser>>();

        for (var i = 0; i < 20; i++)
        {
            var reporter = FakeModel.PersonReporter.Generate();
            await userManager.CreateAsync(new ReporterUser(reporter), faker.Internet.Password());

            var car = FakeModel.Vehicle.Generate();
            car.Class = VehicleClass.B;
            car.Reporter = reporter;
            car.Owner = _mapper.Map<VehicleOwner>(reporter);
            car.Insurance = new VehicleInsurance()
            {
                StartAt = faker.Date.PastDateOnly(5),
                ExpiryAt = faker.Date.FutureDateOnly(4),
                Issuer = aia,
                PolicyId = faker.Vehicle.Vin()
            };

            _dbContext.Vehicles.Add(car);

            var accident = FakeModel.Accident.Generate();
            accident.Cause = faker.PickRandom<AccidentCause>();
            accident.Driver = new Driver()
            {
                Name = faker.Person.FullName,
                Dln = faker.System.AndroidId()
            };
            
            var snapshot =  _mapper.Map<VehicleSnapshot>(car);
            snapshot.AccidentId = accident.Id;
            snapshot.Accident = accident;
            
            
            accident.PoliceApproval = new Approval<PoliceOfficer>()
            {
                IsApproved = true,
                Employee = policeOfficer
            };
            accident.RdaApproval = new Approval<RdaEmployee>()
            {
                IsApproved = true,
                Employee = rdaOfficer
            };
            accident.InsuranceApproval = new Approval<InsuranceEmployee>()
            {
                IsApproved = true,
                Employee = insuranceEmployee
            };
            
            await _dbContext.Accidents.AddAsync(accident);
        }

        await _unitOfWork.CompleteAsync();
    }
}