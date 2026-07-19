using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public sealed class RequestFieldLookupConfiguration : IEntityTypeConfiguration<RequestFieldLookup>
{
    public void Configure(EntityTypeBuilder<RequestFieldLookup> builder)
    {
        builder.ToTable("REQUEST_FIELD_LOOKUPS", "REQ");

        builder.ConfigureAuditableSoftDeleteEntity<RequestFieldLookup, int>();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Schema)
            .IsRequired()
            .HasMaxLength(4);

        builder.Property(x => x.TableName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ValueColumn)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.DisplayColumn)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}