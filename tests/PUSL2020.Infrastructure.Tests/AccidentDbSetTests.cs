using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Infrastructure.Tests.Fixtures;
using PUSL2020.Tests.Shared;
using Xunit;

namespace PUSL2020.Infrastructure.Tests;

public class AccidentDbSetTests  : BaseDbSetTests
{
    public AccidentDbSetTests(InMemoryApplicationDbContextFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ListAllAsync_ReturnsListOfAccidents()
    {
        var accidents = await DbContext.Accidents.ToListAsync();
        Assert.Empty(accidents);
    }
    
    [Fact]
    public async Task Add_AccidentIsAddedToList()
    {
        var reporter = FakeModel.PersonReporter.Generate();
        var faker = new Faker();
        
        var reporterManager = Provider.GetRequiredService<UserManager<ReporterUser>>();
        var result = await reporterManager.CreateAsync(new ReporterUser(reporter), faker.Internet.Password());
        Assert.True(result.Succeeded);
        
        var insurance = FakeModel.Insurance.Generate();
        DbContext.Insurances.Add(insurance);
        
        var vehicle = FakeModel.Vehicle.Generate();
        vehicle.Reporter = reporter;
        vehicle.Insurance = FakeValueObject.VehicleInsurance.Generate();
        vehicle.Insurance.Issuer = insurance;
        DbContext.Vehicles.Add(vehicle);

        
        var accident = FakeModel.Accident.Generate();
        accident.Reporter = reporter;
        accident.Vehicle.AccidentId = accident.Id;
        
        DbContext.Accidents.Add(accident);
        await DbContext.SaveChangesAsync();
        var accidents = await DbContext.Accidents.ToListAsync();
        Assert.NotEmpty(accidents);
    }
}