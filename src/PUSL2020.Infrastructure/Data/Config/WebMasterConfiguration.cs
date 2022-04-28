using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUSL2020.Application.Identity.Models;

namespace PUSL2020.Infrastructure.Data.Config;

public class WebMasterConfiguration : IEntityTypeConfiguration<WebMaster>
{
    public void Configure(EntityTypeBuilder<WebMaster> builder)
    {
       builder.Property(u => u.Id).ValueGeneratedOnAdd();
       builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("WebMaster_UsernameIndex").IsUnique();
       builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        
       builder.Ignore(u => u.Email);
       builder.Ignore(u => u.NormalizedEmail);
       builder.Ignore(u => u.PhoneNumber);
       builder.Ignore(u => u.EmailConfirmed);
       builder.Ignore(u => u.PhoneNumber);
       builder.Ignore(u => u.PhoneNumberConfirmed);
       builder.Ignore(u => u.AccessFailedCount);
       builder.Ignore(u => u.TwoFactorEnabled);
       builder.Ignore(u => u.LockoutEnabled);
       builder.Ignore(u => u.LockoutEnd);
    }
}