using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Config;

public class ReporterConfiguration : 
    IEntityTypeConfiguration<Reporter>, 
    IEntityTypeConfiguration<PersonReporter>,
    IEntityTypeConfiguration<CompanyReporter>,
    IEntityTypeConfiguration<ReporterUser>
{
    public void Configure(EntityTypeBuilder<Reporter> builder)
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
        
        builder.Property(e => e.Created)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Updated)
            .ValueGeneratedOnAddOrUpdate();
    }

    public void Configure(EntityTypeBuilder<PersonReporter> builder)
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
    }

    public void Configure(EntityTypeBuilder<CompanyReporter> builder)
    {
        builder
            .Property(r => r.Crn)
            .HasConversion<Crn.EfCoreValueConverter>();
        builder.HasIndex(p => p.Crn).IsUnique();
    }

    public void Configure(EntityTypeBuilder<ReporterUser> builder)
    {
       builder.Property(r => r.Id)
            .HasField("_id")
            .HasConversion<ReporterId.EfCoreValueConverter>()
            .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

       builder.HasKey(u => u.Id);
            
        // Index for query and uniqueness
       builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("Reporter_EmailIndex").IsUnique();
            
       builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
       builder.Property(u => u.Email).HasMaxLength(256);
       builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

       builder.Ignore(u => u.PasswordHash);
       builder.Ignore(u => u.UserName);
       builder.Ignore(u => u.NormalizedUserName);
       builder.Ignore(u => u.Email);
       builder.Ignore(u => u.PhoneNumber);

       builder.HasOne(r => r.Reporter)
            .WithOne()
            .HasForeignKey<Reporter>(r => r.Id);

       builder.Navigation(u => u.Reporter).AutoInclude();
    }
}