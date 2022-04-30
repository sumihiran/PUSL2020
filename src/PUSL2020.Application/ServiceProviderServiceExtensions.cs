using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PUSL2020.Application;

public static class ServiceProviderServiceExtensions
{
    public static async Task InitialiseAsync(this IServiceProvider provider)
    {
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        var initializers = provider.GetServices<IApplicationInitializer>().OrderBy(initializer =>
        {
            var attribute = initializer.GetType().GetCustomAttribute<OrderAttribute>();
            return attribute?.Order ?? int.MaxValue;
        });
        
        foreach (var initializer in initializers)
        {
            
            await initializer.InitialiseAsync();
        }
    }
}