using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Config;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(i => i.Id)
            .HasConversion<Vid.EfCoreValueConverter>();

        builder.OwnsOne(v => v.Owner, a =>
        {
            a.OwnsOne(o => o.Address);
        });
        builder.OwnsOne(v => v.Insurance, a => { a.HasOne(i => i.Issuer); });
    }
}