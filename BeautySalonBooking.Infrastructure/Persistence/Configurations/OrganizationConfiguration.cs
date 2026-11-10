using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("ORGANIZATIONS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<Organization, long>();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(x => x.LicenseNumber)
            .HasMaxLength(150);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Signature)
            .HasMaxLength(200);

        builder.Property(x => x.Slogan)
            .HasMaxLength(150);

        builder.Property(x => x.Glyph)
            .HasMaxLength(100);

        // 👇 رابطه یک به یک با OrganizationDetail
        builder.HasOne(x => x.Detail)
            .WithOne(d => d.Organization)
            .HasForeignKey<OrganizationDetail>(d => d.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);

        // 👇 رابطه یک به چند با Branch
        builder.HasMany(x => x.Branches)
            .WithOne(b => b.Organization)
            .HasForeignKey(b => b.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);

        // 👇 رابطه یک به چند با OrganizationOwner
        builder.HasMany(x => x.Owners)
            .WithOne(o => o.Organization)
            .HasForeignKey(o => o.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);

        // 👇 رابطه یک به چند با OrganizationMedia
        builder.HasMany(x => x.Media)
            .WithOne(m => m.Organization)
            .HasForeignKey(m => m.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);

        // تنظیم Field Access Mode
        builder.Metadata
            .FindNavigation(nameof(Organization.Owners))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Organization.Media))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Organization.Branches))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}