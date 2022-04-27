using Microsoft.EntityFrameworkCore;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Extensions;

public static class VehicleModelBuilderExtension
{
    public static void ConfigureVehicleModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Institution>(b =>
        {
            b.HasDiscriminator(i => i.InstitutionType)
                .HasValue<Insurance>(InstitutionType.Insurance)
                .HasValue<Institution>(InstitutionType.Police)
                .HasValue<Institution>(InstitutionType.Rda)
                .IsComplete(true);
        });
        
        modelBuilder.Entity<Vehicle>(b =>
        {
            b.Property(i => i.Id)
                .HasConversion<VehicleId.EfCoreValueConverter>();

            b.OwnsOne(v => v.Owner, a =>
            {
                a.OwnsOne(o => o.Name);
                a.OwnsOne(o => o.Address);
            });
            b.OwnsOne(v => v.Insurance, a =>
            {
                a.HasOne(i => i.Issuer);
            });
        });
    }
}