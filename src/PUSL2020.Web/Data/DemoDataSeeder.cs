using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PUSL2020.Application;
using PUSL2020.Application.Data;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Web.Data;

[Order(100)]
public class DemoDataSeeder : IApplicationInitializer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<DemoDataSeeder> _logger;

    public DemoDataSeeder(IServiceProvider serviceProvider, IApplicationDbContext dbContext,
         ILogger<DemoDataSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        _dbContext = dbContext;
        _logger = logger;
    }

    private const string WebMasterUsername = "admin";

    public Task InitialiseAsync() => SeedAsync();

    private async Task SeedAsync()
    {
        await SeedWebMasterAsync();
        await SeedReporterWithVehicleAsync();
    }

    private async Task SeedWebMasterAsync()
    {
        var password = Guid.NewGuid().ToString();
        var admin = new WebMaster
        {
            UserName = WebMasterUsername,
            Id = 100
        };
        var webmasterManager = _serviceProvider.GetRequiredService<UserManager<WebMaster>>();
        await webmasterManager.CreateAsync(admin, password);
        _logger.LogInformation("=======================================");
        _logger.LogInformation("WEBMASTER ACCOUNT");
        _logger.LogInformation("Username: {Username}", WebMasterUsername);
        _logger.LogInformation("Password: {Password}", password);
        _logger.LogInformation("=======================================");
    }

    private async Task SeedReporterWithVehicleAsync()
    {
        var password = Guid.NewGuid().ToString();
        var reporter = new PersonReporter()
        {
            Id = ReporterId.FromGuid("8414f502-0592-41a7-a68d-06d7588c666e"),
            Email = "john@pusl2020.local",
            Name = new Name()
            {
                First = "John",
                Middle = "Maven",
                Last = "Doe"
            },
            Address = new Address()
            {
                Line1 = "Homagama",
                District = District.Colombo,
                ZipCode = 10100
            },
            Created = DateTime.Now,
            Nic = new Nic("199813600988")
        };
        var reporterManager = _serviceProvider.GetRequiredService<UserManager<ReporterUser>>();
        await reporterManager.CreateAsync(new ReporterUser(reporter), password);
        _logger.LogInformation("=======================================");
        _logger.LogInformation("REPORTER ACCOUNT");
        _logger.LogInformation("Name: {Name}", reporter.Name);
        _logger.LogInformation("Email: {Email}", reporter.Email);
        _logger.LogInformation("Password: {Password}", password);
        _logger.LogInformation("=======================================");

        var insurances = await _dbContext.Insurances.ToListAsync();

        var vehicle = new Vehicle
        {
            EngineNo = "1234",
            Class = "Car",
            FuelType = FuelType.Petrol,
            Model = "Audi A4",
            RegisteredAt = DateOnly.FromDateTime(DateTime.Now),
            Make = "Germany",
            Owner = new VehicleOwner()
            {
                Address = reporter.Address.Clone(),
                Name = reporter.Name.ToString(),
                Phone = reporter.PhoneNumber ?? ""
            },
            Insurance = new VehicleInsurance()
            {
                StartAt = DateOnly.FromDateTime(DateTime.Now.Subtract(TimeSpan.FromDays(365))),
                ExpiryAt = DateOnly.FromDateTime(DateTime.Now.AddYears(5)),
                Issuer = insurances.First(),
                PolicyId = "1234"
            },
            Vrn = "AU-213",
            Reporter = reporter
        };

        var vehicleRepository = _serviceProvider.GetRequiredService<IVehicleRepository>();
        await vehicleRepository.AddAsync(vehicle);
    }

    
}
