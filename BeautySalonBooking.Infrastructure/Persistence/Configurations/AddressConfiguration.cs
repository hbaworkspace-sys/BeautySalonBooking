using BeautySalonBooking.Domain.GeographyAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("ADDRESSES", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<Address, long>();

        builder.Property(x => x.RelatedType)
            .IsRequired();

        builder.Property(x => x.FullAddress)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.PostalCode)
            .HasMaxLength(10);

        builder.HasOne(x => x.Region)
            .WithMany()
            .HasForeignKey(x => x.RegionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.RelatedId,
            x.RelatedType
        });
    }
}