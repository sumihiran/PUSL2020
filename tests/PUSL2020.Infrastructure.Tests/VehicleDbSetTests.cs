using System;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Infrastructure.Tests.Fixtures;
using PUSL2020.Tests.Shared;
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

        var reporter = FakeModel.PersonReporter.Generate();
        var faker = new Faker();

        var result = await reporterManager.CreateAsync(new ReporterUser(reporter), faker.Internet.Password());
        
        Assert.True(result.Succeeded);
        
        Assert.Equal(1, await dbContext.PersonReporters.CountAsync());


        var insurance = FakeModel.Insurance.Generate();
        dbContext.Insurances.Add(insurance);

     
        
        var vehicles = await dbContext.Vehicles.ToListAsync();
        Assert.Empty(vehicles);

        var vehicle = FakeModel.Vehicle.Generate();
        vehicle.Reporter = reporter;
        vehicle.Insurance = FakeValueObject.VehicleInsurance.Generate();
        vehicle.Insurance.Issuer = insurance;
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