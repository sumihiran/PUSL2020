using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.ValueObjects;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Infrastructure.Identity;

namespace PUSL2020.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void RegisterInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString(ApplicationDbContext.ConnectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException($"Connection string '{ApplicationDbContext.ConnectionString}' is null or empty",
                connectionString?.GetType().Name);
        }

        services.AddDbContext<ApplicationDbContext>(o =>
        {
            if (environment.IsDevelopment())
            {
                o.UseInMemoryDatabase("PUSL2020");
            }
            else
            {
                o.UseSqlServer(connectionString);
            }
        });
        
        // Identity
        services
            .AddScoped<IUserStore<ReporterUser>, ReporterUserOnlyStore<ApplicationDbContext>>();
        
        services
            .AddScoped<IUserStore<EmployeeUser>, UserOnlyStore<EmployeeUser, ApplicationDbContext, EmployeeId>>();
        
        services
            .AddScoped<IUserStore<WebMaster>, GenericUserStore<WebMaster, ApplicationDbContext, int>>();
    }
}