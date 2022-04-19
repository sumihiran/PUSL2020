using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
// ReSharper disable UnusedType.Global

namespace PUSL2020.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddCommandLine(args)
            .AddEnvironmentVariables()
            .Build();
        
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString(ApplicationDbContext.ConnectionString);
        
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException($"Connection string '{ApplicationDbContext.ConnectionString}' is null or empty",
                connectionString?.GetType().Name);
        }

        builder
            .UseSqlServer(connectionString);

        return new ApplicationDbContext(builder.Options);
    }
}