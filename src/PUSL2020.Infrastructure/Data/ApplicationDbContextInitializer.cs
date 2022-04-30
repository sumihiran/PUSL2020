using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PUSL2020.Application.Data;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data;

public class ApplicationDbContextInitializer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;

    private const string WebMasterUsername = "admin";

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        var seeder = ActivatorUtilities.CreateInstance<MasterDataSeeder>(_serviceProvider);

        await seeder.SeedRdaOffices();
        await seeder.SeedInsurances();
        await seeder.SeedPoliceStations();


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

        var insurances = await _context.Insurances.ToListAsync();

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
        await _context.SaveChangesAsync();
    }
}