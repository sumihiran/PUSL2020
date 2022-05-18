using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PUSL2020.Application.Data;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;
using PUSL2020.Infrastructure.Identity;

namespace PUSL2020.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public const string ConnectionString = "ApplicationConnection";

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ReporterUser> ReporterUsers { get; set; }
    public DbSet<StaffUser> StaffUsers { get; set; }

    public DbSet<Reporter> Reporters { get; set; }
    public DbSet<PersonReporter> PersonReporters { get; set; }
    public DbSet<CompanyReporter> BusinessReporters { get; set; }
    public DbSet<StaffMember> Staff { get; set; }

    private StoreOptions? GetStoreOptions() => this.GetService<IDbContextOptions>()
        .Extensions.OfType<CoreOptionsExtension>()
        .FirstOrDefault()?.ApplicationServiceProvider
        ?.GetService<IOptions<IdentityOptions>>()
        ?.Value?.Stores;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reporter>(builder =>
        {
            builder.HasDiscriminator(r => r.ReporterType)
                .HasValue<PersonReporter>(ReporterType.Person)
                .HasValue<CompanyReporter>(ReporterType.Business);

            builder
                .Property(r => r.Id)
                .HasConversion<ReporterId.EfCoreValueConverter>();

            builder.Property(r => r.ReporterType)
                .HasConversion(new EnumToStringConverter<ReporterType>());
        });

        modelBuilder.Entity<PersonReporter>(builder =>
        {
            builder.Property(p => p.Nic)
                .HasConversion<Nic.EfCoreValueConverter>();
            builder.Property(p => p.DriverLicenseId)
                .HasConversion<DriverLicenseId.EfCoreValueConverter>();
        });

        modelBuilder.Entity<CompanyReporter>()
            .Property(b => b.CompanyNo)
            .HasConversion<CompanyNo.EfCoreValueConverter>();


        modelBuilder.Entity<ReporterUser>(b =>
        {
            b.Property(r => r.Id)
                .HasField("_id")
                .HasConversion<ReporterId.EfCoreValueConverter>()
                .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

            b.HasKey(u => u.Id);
            b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);

            b.Ignore(u => u.PasswordHash);
            b.Ignore(u => u.UserName);
            b.Ignore(u => u.Email);
            b.Ignore(u => u.PhoneNumber);

            b.HasOne(r => r.Reporter)
                .WithOne()
                .HasForeignKey<Reporter>(r => r.Id);

            b.Navigation(u => u.Reporter).AutoInclude();
        });

        modelBuilder.Entity<Institution>(b =>
        {
            b.Property(r => r.Id)
                .HasConversion<InstitutionId.EfCoreValueConverter>();

            b.HasKey(i => i.Id);
        });
        
        modelBuilder.Entity<StaffMember>(b =>
        {
            b.Property(r => r.Id)
                .HasConversion<StaffMemberId.EfCoreValueConverter>();


            b.HasOne(m => m.Office)
                .WithMany();

            b.Navigation(m => m.Office).AutoInclude();
        });

        modelBuilder.Entity<StaffUser>(b =>
        {
            b.Property(r => r.Id)
                .HasField("_id")
                .HasConversion<StaffMemberId.EfCoreValueConverter>()
                .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

            b.HasKey(u => u.Id);
            b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);

            b.Ignore(u => u.PasswordHash);
            b.Ignore(u => u.UserName);
            b.Ignore(u => u.Email);
            b.Ignore(u => u.PhoneNumber);
            b.Ignore(u => u.EmailConfirmed);
            b.Ignore(u => u.PhoneNumber);
            b.Ignore(u => u.PhoneNumberConfirmed);

            b.HasOne(r => r.StaffMember)
                .WithOne()
                .HasForeignKey<StaffMember>(r => r.Id);
            b.Navigation(u => u.StaffMember).AutoInclude();
        });

        modelBuilder.Entity<Institution>()
            .HasData(new Institution()
            {
                Id = new InstitutionId(Guid.Parse("9202c023-b0e8-4cf6-b8d4-2e932ef3c59c")),
                InstitutionType = InstitutionType.Police,
                Name = "Regional Police",
                Address = "Homagama",
                PhoneNumber = "0112233456"
            });
    }
}