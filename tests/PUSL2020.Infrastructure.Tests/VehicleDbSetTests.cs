using System;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Infrastructure.Tests.Fixtures;
using Xunit;

namespace PUSL2020.Infrastructure.Tests;

public class VehicleDbSetTests : IClassFixture<InMemoryApplicationDbContextFixture>, IAsyncLifetime
{
    private readonly InMemoryApplicationDbContextFixture _fixture;

    public VehicleDbSetTests(InMemoryApplicationDbContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Vehicle_Add_AddedToDatabase()
    {
        var dbContext = Provider!.GetRequiredService<ApplicationDbContext>();
        var reporterManager = Provider!.GetRequiredService<UserManager<ReporterUser>>();
        var name = new Faker<Name>()
            .RuleFor(b => b.First, f => f.Person.FirstName)
            .RuleFor(b => b.Middle, f => "")
            .RuleFor(b => b.Last, f => f.Person.LastName);
        
        var address = new Faker<Address>()
            .RuleFor(b => b.Line1, f => f.Address.BuildingNumber())
            .RuleFor(b => b.Line2, f => "")
            .RuleFor(b => b.Street, f => f.Address.StreetName())
            .RuleFor(b => b.City, f => f.Address.City())
            .RuleFor(b => b.District, f => f.Address.State())
            .RuleFor(b => b.ZipCode, f => f.Random.Int(10000,99000));
        
        var personFaker = new Faker<PersonReporter>()
            .RuleFor(b => b.Nic, f => new Nic($"{f.Random.Int(5500000,999999999)}V"))
            .RuleFor(b => b.Email, f => f.Person.Email)
            .RuleFor(b => b.Name, name)
            .RuleFor(b => b.Address, address);
        
        var reporter = personFaker.Generate();
        var faker = new Faker();

        var result = await reporterManager.CreateAsync(new ReporterUser(reporter), faker.Internet.Password(10));
        
        Assert.True(result.Succeeded);
        
        Assert.Equal(1, await dbContext.PersonReporters.CountAsync());

        var insuranceFaker = new Faker<Insurance>()
            .RuleFor(i => i.Address, f => address)
            .RuleFor(i => i.Name, f => f.Company.CompanyName())
            .RuleFor(i => i.PhoneNumber, f => f.Person.Phone);
        var insurance = insuranceFaker.Generate();
        dbContext.Insurances.Add(insurance);

        var vehicleFaker = new Faker<Vehicle>()
            .RuleFor(v => v.Make, f=> f.Vehicle.Manufacturer())
            .RuleFor(v => v.EngineNo, f=> f.Vehicle.Vin())
            .RuleFor(v => v.Class, f => f.Vehicle.Type())
            .RuleFor(v => v.FuelType, f => f.PickRandom<FuelType>())
            .RuleFor(v => v.Model, f => f.Vehicle.Model())
            .RuleFor(v => v.RegistrationNo, f => f.Random.String(10))
            .RuleFor(v => v.RegisteredAt, f => f.Date.PastDateOnly(10));
        
        var vehicles = await dbContext.Vehicles.ToListAsync();
        Assert.Empty(vehicles);

        var vehicle = vehicleFaker.Generate();
        vehicle.Reporter = reporter;
        vehicle.Insurance = new VehicleInsurance()
        {
            PolicyId = faker.Finance.Account(8),
            StartAt = faker.Date.RecentDateOnly(),
            ExpiryAt = faker.Date.FutureDateOnly(5),
            Issuer = insurance
        };

        dbContext.Vehicles.Add(vehicle);
        await dbContext.SaveChangesAsync();
        
        vehicles = await dbContext.Vehicles.ToListAsync();
        
        Assert.NotEmpty(vehicles);
    }
    
    private IServiceScope? Scope { get; set; }
    private IServiceProvider? Provider { get; set; }


    public Task InitializeAsync()
    {
        Scope = _fixture.Host.Services.CreateScope();
        Provider = Scope.ServiceProvider;
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        Scope?.Dispose();
        return Task.CompletedTask;
    }
}