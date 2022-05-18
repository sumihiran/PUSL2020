using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PUSL2020.Domain.ValueObjects;
using PUSL2020.Infrastructure.Data;
using PUSL2020.Infrastructure.Identity;

namespace PUSL2020.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
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
        
        AddReporterIdentity(services);
        services.AddScoped<IUserStore<ReporterUser>, UserOnlyStore<ReporterUser, ApplicationDbContext, ReporterId>>();

        AddStaffIdentity(services)
            .AddSignInManager();
        
        services.AddScoped<IUserStore<StaffUser>, UserOnlyStore<StaffUser, ApplicationDbContext, StaffMemberId>>();
        
        return services;
    }

    private static IdentityBuilder AddReporterIdentity(IServiceCollection services)
    {
        return services.AddIdentityCore<ReporterUser>(o => { o.Stores.MaxLengthForKeys = 128; })
            .AddDefaultTokenProviders();
    }

    private static IdentityBuilder AddStaffIdentity(IServiceCollection services)
    {
        return services.AddIdentityCore<StaffUser>(o =>
            {
                o.SignIn.RequireConfirmedEmail = false;
                o.SignIn.RequireConfirmedPhoneNumber = false;
                o.Stores.MaxLengthForKeys = 128;
            })
            .AddDefaultTokenProviders();
    }
}