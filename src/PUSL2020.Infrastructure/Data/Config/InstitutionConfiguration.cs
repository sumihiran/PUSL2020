using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUSL2020.Domain.Entities.Institutions;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Config;

public class InstitutionConfiguration: 
    IEntityTypeConfiguration<PoliceStation>,
    IEntityTypeConfiguration<RdaOffice>,
    IEntityTypeConfiguration<Insurance>
{
    public void Configure(EntityTypeBuilder<PoliceStation> builder)
    {
       builder.Property(i => i.Id)
            .HasConversion<InstitutionId.EfCoreValueConverter>();

       builder.Property(i => i.Id);
       builder.HasKey(i => i.Id);
       builder.HasMany(i => i.Employees);
    }

    public void Configure(EntityTypeBuilder<RdaOffice> builder)
    {
       builder.Property(i => i.Id)
            .HasConversion<InstitutionId.EfCoreValueConverter>();

       builder.Property(i => i.Id);
       builder.HasKey(i => i.Id);
       builder.HasMany(i => i.Employees);
    }

    public void Configure(EntityTypeBuilder<Insurance> builder)
    {
       builder.Property(i => i.Id)
            .HasConversion<InstitutionId.EfCoreValueConverter>();

       builder.Property(i => i.Id);
       builder.HasKey(i => i.Id);
       builder.HasMany(i => i.Employees);
    }
}