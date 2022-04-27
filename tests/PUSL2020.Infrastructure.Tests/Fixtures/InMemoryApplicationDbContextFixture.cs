using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PUSL2020.Application;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Infrastructure.Identity;
using Xunit;

namespace PUSL2020.Infrastructure.Tests.Fixtures;

public class InMemoryApplicationDbContextFixture : ICollectionFixture<MinIoFixture>, IAsyncLifetime, IDisposable
{
    public readonly IHost Host = Microsoft.Extensions.Hosting.Host
        .CreateDefaultBuilder()
        .ConfigureServices((ctx, services) =>
        {
            services.AddAuthenticationDefaults();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            });
            services.AddReporterIdentity();
            services
                .AddScoped<IUserStore<ReporterUser>, ReporterUserOnlyStore<ApplicationDbContext>>();

            // remove ApplicationDbContext
            services.RemoveAll(typeof(ApplicationDbContext));

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase(Assembly.GetExecutingAssembly().GetName().ToString()));

            services.AddSingleton<ILogger>(serviceProvider =>
                serviceProvider.GetRequiredService<ILogger<MinIoFixture>>());
        })
        .Build();


    public Task InitializeAsync()
    {
        return Host.StartAsync();
    }

    public Task DisposeAsync()
    {
        return Host.StopAsync();
    }

    public void Dispose()
    {
        Host.Dispose();
    }
}