using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Application.Identity;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Application;

public static class ServiceCollectionExtensions
{
    public static ApplicationServiceBuilder AddApplicationServices(this IServiceCollection services)
    {
        return new ApplicationServiceBuilder(services);
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
    
    public static IdentityBuilder AddWebMasterIdentity(this IServiceCollection services)
    {
        return services.AddIdentityCore<WebMaster>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<WebMasterUserClaimsPrincipalFactory>();
    }
    
}