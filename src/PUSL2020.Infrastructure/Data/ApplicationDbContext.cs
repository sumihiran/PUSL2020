using Microsoft.EntityFrameworkCore;
using PUSL2020.Application.Data;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.Entities.Institutions;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public const string ConnectionString = "DefaultConnection";
    
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Reporter> Reporters { get; set; }
    public DbSet<PersonReporter> PersonReporters { get; set; }
    public DbSet<CompanyReporter> CompanyReporters { get; set; }
    
    public DbSet<PoliceStation> PoliceStations { get; set; }
    public DbSet<RdaOffice> RdaOffices { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeUser> EmployeeUsers { get; set; }
    public DbSet<RdaEmployee> RdaEmployees { get; set; }
    public DbSet<PoliceOfficer> PoliceOfficers { get; set; }
    public DbSet<InsuranceEmployee> InsuranceEmployees { get; set; }
    
    public DbSet<WebMaster> WebMasters { get; set; }
    
  
    public DbSet<ImageResource> Images { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Accident> Accidents { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        
        configurationBuilder
            .Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Ignore<Address>();
        
        modelBuilder.Entity<ImageResource>(b =>
        {
            b.HasKey(i => i.Id);
        });
        
        
    }
}