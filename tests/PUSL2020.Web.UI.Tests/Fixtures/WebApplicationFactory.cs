using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Minio.AspNetCore;

namespace PUSL2020.Web.UI.Tests.Fixtures;

public class WebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((ctx, services) =>
        {
            var minioOptions = new MinioOptions();
            ctx.Configuration.GetRequiredSection("Minio").Bind(minioOptions);
            
            // var dbServiceDescriptor = new ServiceDescriptor(typeof(DbContextOptions<ApplicationDbContext>), null!);
            // services
            //     .Remove(dbServiceDescriptor);
            // services.AddDbContext<ApplicationDbContext>((sp, options) =>
            //         options.UseSqlServer(ctx.Configuration.GetConnectionString("DefaultConnection"),
            //             b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        });
    }
}