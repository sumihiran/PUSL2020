using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using PUSL2020.Application.Identity;

namespace PUSL2020.Application;

public static class IdentityEntityFrameworkBuilderExtensions
{
    public static AuthenticationBuilder AddReporterAuthentication(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        return services.AddAuthentication();
    }
    
    public static AuthenticationBuilder AddStaffAuthentication(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        return services.AddAuthentication(StaffAuthenticationDefaults.AuthenticationScheme);
    }
}