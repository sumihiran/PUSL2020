using Microsoft.EntityFrameworkCore;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.Entities.Institutions;
using PUSL2020.Domain.Entities.Vehicles;

namespace PUSL2020.Application.Data;

public interface IApplicationDbContext
{
    #region Reporters
    public DbSet<Reporter> Reporters { get; set; }
    public DbSet<PersonReporter> PersonReporters { get; set; }
    public DbSet<CompanyReporter> CompanyReporters { get; set; }

    #endregion

    
    
    public DbSet<PoliceStation> PoliceStations { get; set; }
    public DbSet<RdaOffice> RdaOffices { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    
    #region Employees

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeUser> EmployeeUsers { get; set; }
    
    public DbSet<RdaEmployee> RdaEmployees { get; set; }
    public DbSet<PoliceOfficer> PoliceOfficers { get; set; }
    public DbSet<InsuranceEmployee> InsuranceEmployees { get; set; }

    #endregion
    
   
    public DbSet<WebMaster> WebMasters { get; set; }
    
    public DbSet<ImageResource> Images { get; set; }
    
    public DbSet<Vehicle> Vehicles { get; set; }
    
    public DbSet<Accident> Accidents { get; set; }

    public DbSet<T> Set<T>() where T : class;
}