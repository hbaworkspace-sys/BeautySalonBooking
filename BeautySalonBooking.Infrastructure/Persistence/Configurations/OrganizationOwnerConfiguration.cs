using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class OrganizationOwnerConfiguration : IEntityTypeConfiguration<OrganizationOwner>
{
    public void Configure(EntityTypeBuilder<OrganizationOwner> builder)
    {
        builder.ToTable("ORGANIZATION_OWNERS", "GT");

        builder.ConfigureAuditableEntity<OrganizationOwner, long>();

        builder.Property(x => x.IsPrimary)
            .IsRequired();

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.Owners)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.OwnerUser)
            .WithMany(x => x.OrganizationOwners)
            .HasForeignKey(x => x.OwnerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.OwnerUserId
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.IsPrimary
        })
        .HasFilter("[IsPrimary] = 1")
        .IsUnique();
    }
}