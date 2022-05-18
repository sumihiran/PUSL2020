using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PUSL2020.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.AddReporterAuthentication();
        services.AddStaffAuthentication();
        
        return services;
    }
    
}