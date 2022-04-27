using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Minio.AspNetCore;
using Xunit;

namespace PUSL2020.Infrastructure.Tests.Fixtures;

public class MinIoFixture : ICollectionFixture<MinIoFixture>, IAsyncLifetime, IDisposable
{
    public readonly IHost Host = Microsoft.Extensions.Hosting.Host 
        .CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            services.AddMinio(opt =>
            {
                opt.Endpoint = "localhost:9000";
                opt.AccessKey = "XEJDHAZNKDSK0I8PIRNRHTPP";
                opt.SecretKey = "TEiP4NlKOwjnHe_IAl13nCzNMVKZSP-tfZevPjVg89t8IitA";
                opt.Region = "lk";
            });
            services.AddSingleton<ILogger>(serviceProvider => serviceProvider.GetRequiredService<ILogger<MinIoFixture>>());
        })
        .Build();

    
    public Task InitializeAsync()
    {
        return Host.StartAsync();
    }

    public Task DisposeAsync()
    {
        return Host.StopAsync();
    }

    public void Dispose()
    {
        Host.Dispose();
    }
}