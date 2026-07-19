using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class RequestFieldConfiguration : IEntityTypeConfiguration<RequestField>
{
    public void Configure(EntityTypeBuilder<RequestField> builder)
    {
        builder.ToTable("REQUEST_FIELDS", "REQ");

        builder.ConfigureAuditableSoftDeleteEntity<RequestField, int>();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.FieldType)
            .IsRequired();

        builder.Property(x => x.IsRequired)
            .IsRequired();

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.Property(x => x.DefaultValue)
            .HasMaxLength(50);

        builder.Property(x => x.Placeholder)
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Group)
            .IsRequired();

        builder.HasOne(x => x.RequestType)
            .WithMany(x => x.RequestFields)
            .HasForeignKey(x => x.RequestTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Lookup)
            .WithMany(x => x.RequestFields)
            .HasForeignKey(x => x.LookupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.RequestTypeId,
            x.Key
        })
        .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(RequestField.RequestDetails))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}