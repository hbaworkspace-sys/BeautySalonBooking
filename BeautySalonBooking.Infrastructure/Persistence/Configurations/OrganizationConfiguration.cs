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