using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Application.Identity;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Application;

public static class ServiceCollectionExtensions
{
    public static void RegisterApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        throw new NotImplementedException();
    }
    
    public static AuthenticationBuilder AddAuthenticationDefaults(this IServiceCollection services)
    {
        return services.AddAuthentication(o =>
        {
            o.DefaultScheme = IdentityConstants.ApplicationScheme;
            o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        });
    }

    public static IdentityBuilder AddReporterIdentity(this IServiceCollection services)
    {
        return services.AddIdentityCore<ReporterUser>()
            .AddDefaultTokenProviders();
    }
    
    public static IdentityBuilder AddEmployeeIdentity(this IServiceCollection services)
    {
        return services.AddIdentityCore<EmployeeUser>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<EmployeeUserClaimsPrincipalFactory>();
    }
    
}