using Microsoft.EntityFrameworkCore;
using PUSL2020.Domain.Entities;

namespace PUSL2020.Application.Data;

public interface IApplicationDbContext 
{
   public DbSet<Reporter> Reporters { get; set; }
   
   public DbSet<PersonReporter> PersonReporters { get; set; }
   public DbSet<CompanyReporter> BusinessReporters { get; set; }
   
   public DbSet<StaffMember> Staff { get; set; }
}