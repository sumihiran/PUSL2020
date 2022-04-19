using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MultiScheme.Domain;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<EmployeeUser> _employeeManager;
    private readonly UserManager<ReporterUser> _userManager;

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context, 
        UserManager<EmployeeUser> employeeManager, 
        UserManager<ReporterUser> userManager)
    {
        _logger = logger;
        _context = context;
        _employeeManager = employeeManager;
        _userManager = userManager;
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
        var police = new Institution()
        {
            Id = new InstitutionId(Guid.Parse("9202c023-b0e8-4cf6-b8d4-2e932ef3c59c")),
            InstitutionType = InstitutionType.Police,
            Name = "Regional Police",
            Address = new Address("Homagama Police Station", string.Empty, "Walgama-Diyagama Road", "Homagama",
                "Colombo", 10200),
            PhoneNumber = "0112233456"
        };

        _context.Institutions.Add(police);

        await _context.SaveChangesAsync();
        
        var staffUser = new EmployeeUser(new Employee()
        {
            DisplayName = "D D N S Bandara",
            Office = police,
            UserName = "sumihiran"
        });
        await _employeeManager.CreateAsync(staffUser, "S3cr3t!");
        
        var reporterUser = new ReporterUser(new PersonReporter()
        {
            Email = "nuwan@gmail.com",
            Name = new Name()
            {
                First = "Nuwan",
                Middle = "Sumihiran",
                Last = "Bandara"
            },
            Nic = new Nic("951360988V")
        });
        await _userManager.CreateAsync(reporterUser, "P@55w0rd");
        
    }

}