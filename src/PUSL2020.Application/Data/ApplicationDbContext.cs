using Microsoft.EntityFrameworkCore;
using MultiScheme.Domain;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;

namespace PUSL2020.Application.Data;

public interface IApplicationDbContext 
{
   public DbSet<Reporter> Reporters { get; set; }
   public DbSet<PersonReporter> PersonReporters { get; set; }
   public DbSet<CompanyReporter> CompanyReporters { get; set; }
   public DbSet<Employee> Employees { get; set; }
   public DbSet<WebMaster> WebMasters { get; set; }
   public DbSet<Institution> Institutions { get; set; }
}