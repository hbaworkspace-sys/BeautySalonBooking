using BeautySalonBooking.Domain.ContactAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.ToTable("PHONENUMBERS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<PhoneNumber, long>();

        builder.Property(x => x.RelatedType)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Number)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.IsDefault)
            .IsRequired();

        builder.Property(x => x.IsVerified)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.RelatedId,
            x.RelatedType,
            x.Number
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.RelatedId,
            x.RelatedType,
            x.IsDefault
        })
        .HasFilter("[IsDefault] = 1")
        .IsUnique();
    }
}