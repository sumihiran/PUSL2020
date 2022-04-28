using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Infrastructure.Data;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<EmployeeUser> _employeeManager;
    private readonly UserManager<ReporterUser> _reporterManager;
    private readonly UserManager<WebMaster> _webmasterManager;

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context,
        UserManager<EmployeeUser> employeeManager,
        UserManager<ReporterUser> reporterManager,
        UserManager<WebMaster> webmasterManager
    )
    {
        _logger = logger;
        _context = context;
        _employeeManager = employeeManager;
        _reporterManager = reporterManager;
        _webmasterManager = webmasterManager;
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

    public async Task SeedAsync()
    {
        var admin = new WebMaster()
        {
            UserName = "admin"
        };
        await _webmasterManager.CreateAsync(admin, "Pa$$w0rd");
    }
}