using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minio.AspNetCore;
using PUSL2020.Application;
using PUSL2020.Application.Data;
using PUSL2020.Application.Data.Impl;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Application.Services;
using PUSL2020.Application.Services.Impl;
using PUSL2020.Domain.ValueObjects;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Infrastructure.Identity;
using PUSL2020.Infrastructure.Services;

namespace PUSL2020.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void RegisterInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {

        AddDbContextServices(services, configuration);
        AddIdentityStores(services);
        
        services.AddTransient<IReporterRepository, ReporterRepository>();
        services.AddTransient<IVehicleRepository, VehicleRepository>();
        services.AddTransient<IVehicleService, VehicleService>();
        
        // Object Storage
        var minioOptions = new MinioOptions();
        configuration.GetRequiredSection("Minio").Bind(minioOptions);
        services.AddMinio(opt =>
        {
            opt.Endpoint = minioOptions.Endpoint;
            opt.AccessKey = minioOptions.AccessKey;
            opt.SecretKey = minioOptions.SecretKey;
            opt.Region = minioOptions.Region;
        });
    }

    private static void AddDbContextServices(IServiceCollection services,  IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            if (configuration.UseInMemory())
            {
                o.UseInMemoryDatabase("PUSL2020");
            }
            else
            {
                var connectionString = configuration.GetConnectionString(ApplicationDbContext.ConnectionString);

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new ArgumentException($"Connection string '{ApplicationDbContext.ConnectionString}' is null or empty",
                        connectionString?.GetType().Name);
                }
                
                o.UseSqlServer(connectionString);
            }
        });
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IApplicationInitializer, ApplicationDbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddIdentityStores(IServiceCollection services)
    {
        services.AddScoped<IUserStore<ReporterUser>, ReporterUserOnlyStore<ApplicationDbContext>>();
        services.AddScoped<IUserStore<EmployeeUser>, UserOnlyStore<EmployeeUser, ApplicationDbContext, EmployeeId>>();
        services.AddScoped<IUserStore<WebMaster>, GenericUserStore<WebMaster, ApplicationDbContext, int>>();
    }
}