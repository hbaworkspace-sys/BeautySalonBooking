using BeautySalonBooking.Domain.GeographyAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("REGIONS", "BT");

        builder.ConfigureAuditableSoftDeleteEntity<Region, int>();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Metadata
            .FindNavigation(nameof(Region.Children))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
