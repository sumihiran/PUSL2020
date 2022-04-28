using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUSL2020.Application.Identity.Models;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Config;

public class EmployeeConfiguration: 
    IEntityTypeConfiguration<Employee>,
    IEntityTypeConfiguration<EmployeeUser>,
    IEntityTypeConfiguration<RdaEmployee>,
    IEntityTypeConfiguration<PoliceOfficer>,
    IEntityTypeConfiguration<InsuranceEmployee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.Id)
            .HasConversion<EmployeeId.EfCoreValueConverter>();

        builder.Property(e => e.Created)
            .ValueGeneratedOnAdd();
    }

    public void Configure(EntityTypeBuilder<EmployeeUser> builder)
    {
       builder.Property(u => u.Id)
            .HasField("_id")
            .HasConversion<EmployeeId.EfCoreValueConverter>()
            .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

       builder.HasKey(u => u.Id);
       builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("Employee_UsernameIndex").IsUnique();
       builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

       builder.Ignore(u => u.PasswordHash);
       builder.Ignore(u => u.UserName);
       builder.Ignore(u => u.Email);
       builder.Ignore(u => u.NormalizedEmail);
       builder.Ignore(u => u.PhoneNumber);
       builder.Ignore(u => u.EmailConfirmed);
       builder.Ignore(u => u.PhoneNumber);
       builder.Ignore(u => u.PhoneNumberConfirmed);

       builder.HasOne(u => u.Employee)
            .WithOne()
            .HasForeignKey<Employee>(e => e.Id);
       builder.Navigation(u => u.Employee).AutoInclude();
    }

    public void Configure(EntityTypeBuilder<RdaEmployee> builder)
    {
        builder.ToTable("Rda_Employees");
    }

    public void Configure(EntityTypeBuilder<PoliceOfficer> builder)
    {
        builder.ToTable("Police_Officers");
    }

    public void Configure(EntityTypeBuilder<InsuranceEmployee> builder)
    {
        builder.ToTable("Insurance_Employees");
    }
}