using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minio.AspNetCore;
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
        
        // Database
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            if (environment.IsDevelopment())
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
        services.AddTransient<IReporterRepository, ReporterRepository>();
        services.AddTransient<IVehicleRepository, VehicleRepository>();
        services.AddTransient<IVehicleService, VehicleService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Identity
        services
            .AddScoped<IUserStore<ReporterUser>, ReporterUserOnlyStore<ApplicationDbContext>>();
        
        services
            .AddScoped<IUserStore<EmployeeUser>, UserOnlyStore<EmployeeUser, ApplicationDbContext, EmployeeId>>();
        
        services
            .AddScoped<IUserStore<WebMaster>, GenericUserStore<WebMaster, ApplicationDbContext, int>>();
        
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
}