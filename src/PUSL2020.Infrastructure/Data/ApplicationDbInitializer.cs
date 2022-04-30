using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PUSL2020.Application;

namespace PUSL2020.Infrastructure.Data;

[Order(int.MinValue)]
public class ApplicationDbInitializer : IApplicationInitializer
{
    private readonly ILogger<ApplicationDbInitializer> _logger;
    private readonly ApplicationDbContext _context;
    
    public ApplicationDbInitializer(
        ILogger<ApplicationDbInitializer> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}