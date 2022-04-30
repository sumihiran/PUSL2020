using Microsoft.Extensions.Configuration;

namespace PUSL2020.Infrastructure;

public static class HostEnvironmentEnvExtensions
{
    public static bool UseInMemory(this IConfiguration configuration)
    {
        return configuration.GetValue("UseInMemory", false);
    }
}