using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PUSL2020.Application.Data;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;
using PUSL2020.Infrastructure.Data.Extensions;

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
   
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeUser> EmployeeUsers { get; set; }
    public DbSet<WebMaster> WebMasters { get; set; }

    public DbSet<Institution> Institutions { get; set; }
    public DbSet<ImageResource> Images { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Insurance> Insurances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Reporter

        modelBuilder.Entity<Reporter>(builder =>
        {
            builder
                .Property(r => r.Id)
                .HasConversion<ReporterId.EfCoreValueConverter>();
                
            builder.HasKey(r => r.Id);
            builder.Property(r => r.ReporterType)
                .HasConversion(new EnumToStringConverter<ReporterType>());
            
            builder.HasDiscriminator(r => r.ReporterType)
                .HasValue<PersonReporter>(ReporterType.Person)
                .HasValue<CompanyReporter>(ReporterType.Company);

            builder.OwnsOne(r => r.Address);
        });
        
        modelBuilder.Entity<PersonReporter>(builder =>
        {
            builder.Property(p => p.Nic)
                .HasConversion<Nic.EfCoreValueConverter>();
            builder.Property(p => p.DriverLicenseId)
                .HasConversion<DriverLicenseId.EfCoreValueConverter>();
            builder.HasIndex(p => p.DriverLicenseId).IsUnique();
            builder.HasIndex(p => p.Nic).IsUnique();
            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.First).HasColumnName("FirstName");
                name.Property(n => n.Middle).HasColumnName("MiddleName");
                name.Property(n => n.Last).HasColumnName("LastName");
            });
            
        });
        
        modelBuilder.Entity<CompanyReporter>(builder =>
        {
            builder
                .Property(r => r.Crn)
                .HasConversion<Crn.EfCoreValueConverter>();
            builder.HasIndex(p => p.Crn).IsUnique();
        });

        modelBuilder.Entity<ReporterUser>(b =>
        {
            b.Property(r => r.Id)
                .HasField("_id")
                .HasConversion<ReporterId.EfCoreValueConverter>()
                .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

            b.HasKey(u => u.Id);
            
            // Index for query and uniqueness
            b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("Reporter_EmailIndex").IsUnique();
            
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);

            b.Ignore(u => u.PasswordHash);
            b.Ignore(u => u.UserName);
            b.Ignore(u => u.NormalizedUserName);
            b.Ignore(u => u.Email);
            b.Ignore(u => u.PhoneNumber);

            b.HasOne(r => r.Reporter)
                .WithOne()
                .HasForeignKey<Reporter>(r => r.Id);

            b.Navigation(u => u.Reporter).AutoInclude();
        });
        
        #endregion
        
        modelBuilder.Entity<Institution>(b =>
        {
            b.Property(i => i.Id)
                .HasConversion<InstitutionId.EfCoreValueConverter>();

            b.Property(i => i.Id);
            b.HasKey(i => i.Id);
            b.OwnsOne(i => i.Address);
        });
        
        #region Employee
        modelBuilder.Entity<Employee>(b =>
        {
            b.Property(e => e.Id)
                .HasConversion<EmployeeId.EfCoreValueConverter>();

            
            b.HasOne(e => e.Office)
                .WithMany();

            b.Navigation(e => e.Office).AutoInclude();
        });
        
        
        modelBuilder.Entity<EmployeeUser>(b =>
        {
            b.Property(u => u.Id)
                .HasField("_id")
                .HasConversion<EmployeeId.EfCoreValueConverter>()
                .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

            b.HasKey(u => u.Id);
            b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("Employee_UsernameIndex").IsUnique();
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            b.Ignore(u => u.PasswordHash);
            b.Ignore(u => u.UserName);
            b.Ignore(u => u.Email);
            b.Ignore(u => u.NormalizedEmail);
            b.Ignore(u => u.PhoneNumber);
            b.Ignore(u => u.EmailConfirmed);
            b.Ignore(u => u.PhoneNumber);
            b.Ignore(u => u.PhoneNumberConfirmed);

            b.HasOne(u => u.Employee)
                .WithOne()
                .HasForeignKey<Employee>(e => e.Id);
            b.Navigation(u => u.Employee).AutoInclude();
        });
        
        #endregion
        
        modelBuilder.Entity<WebMaster>(b =>
        {
            b.Property(u => u.Id).ValueGeneratedOnAdd();
            b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("WebMaster_UsernameIndex").IsUnique();
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        
            b.Ignore(u => u.Email);
            b.Ignore(u => u.NormalizedEmail);
            b.Ignore(u => u.PhoneNumber);
            b.Ignore(u => u.EmailConfirmed);
            b.Ignore(u => u.PhoneNumber);
            b.Ignore(u => u.PhoneNumberConfirmed);
            b.Ignore(u => u.AccessFailedCount);
            b.Ignore(u => u.TwoFactorEnabled);
            b.Ignore(u => u.LockoutEnabled);
            b.Ignore(u => u.LockoutEnd);
        });

        modelBuilder.Entity<ImageResource>(b =>
        {
            b.HasKey(i => i.Id);
        });
        
        modelBuilder.ConfigureVehicleModel();
    }
}