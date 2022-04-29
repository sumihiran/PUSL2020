using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PUSL2020.Domain.Enums;
using PUSL2020.Infrastructure.Data;
using Xunit;

namespace PUSL2020.Infrastructure.Tests;

public class MasterDataSeederTests : IAsyncLifetime
{
    private readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureServices((ctx, services) =>
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase(Assembly.GetExecutingAssembly().GetName().ToString()));

            services.AddSingleton<ILogger>(serviceProvider =>
                serviceProvider.GetRequiredService<ILogger<MasterDataSeederTests>>());

            services.AddTransient<MasterDataSeeder>();
        })
        .Build();
    
    private IServiceScope Scope { get; set; }
    
    private ApplicationDbContext DbContext { get; set; }

    private MasterDataSeeder Seeder { get; set; }

    [Fact]
    public async Task SeedPoliceStations_ReturnsListOfPoliceStations()
    {
        await Seeder.SeedPoliceStations();
        var stations = await DbContext.PoliceStations.ToListAsync();
        Assert.NotEmpty(stations);
    }
    
    [Fact]
    public async Task SeedInsurances_ReturnsListOfInsurance()
    {
        await Seeder.SeedInsurances();
        
        var insurances = await DbContext.Insurances.ToListAsync();
        Assert.NotEmpty(insurances);
    }
    
    [Fact]
    public async Task SeedRdaOffices_ReturnsListOfRdaOffices()
    {
        await Seeder.SeedRdaOffices();
        
        var offices = await DbContext.RdaOffices.ToListAsync();
        Assert.NotEmpty(offices);
        Assert.Equal(Enum.GetValues<District>().Length, offices.Count);
    }
    
    public Task InitializeAsync()
    {
        Scope = _host.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        Seeder = Scope.ServiceProvider.GetRequiredService<MasterDataSeeder>();
        return _host.StartAsync();
    }

    public async Task DisposeAsync()
    {
        Scope.Dispose();
        await _host.StopAsync();
        _host.Dispose();
    }

}