using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Config;

public class AccidentConfiguration : IEntityTypeConfiguration<Accident>
{
    public void Configure(EntityTypeBuilder<Accident> builder)
    {
       builder.Property(a => a.Id)
            .HasConversion<RefId.EfCoreValueConverter>();
       
       builder.HasOne(a => a.Reporter);
       
       builder.OwnsOne(a => a.Driver);
       builder.OwnsOne(a => a.Location, lc =>
       {
           lc.Property(l => l.Latitude).HasPrecision(10, 8);
           lc.Property(l => l.Longitude).HasPrecision(11, 8);
       });

       builder.OwnsOne(a => a.Vehicle, b =>
       {
           b.HasKey(v => v.AccidentId);
           b.OwnsOne(v => v.Owner);
           b.OwnsOne(v => v.Insurance, vb => vb.HasOne(vs => vs.Issuer));
           
           b.ToTable("Vehicle_Snapshots");
       });
       
       builder.OwnsOne(a => a.RdaApproval, b =>
       {
           b.HasOne(r => r.Employee);
       });
       
       builder.OwnsOne(a => a.PoliceApproval, b =>
       {
           b.HasOne(r => r.Employee);
       });
       
       builder.OwnsOne(a => a.InsuranceApproval, b =>
       {
           b.HasOne(r => r.Employee);
       });
    }
}