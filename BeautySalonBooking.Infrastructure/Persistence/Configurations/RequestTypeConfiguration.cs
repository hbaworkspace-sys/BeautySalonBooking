using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class RequestTypeConfiguration : IEntityTypeConfiguration<RequestType>
{
    public void Configure(EntityTypeBuilder<RequestType> builder)
    {
        builder.ToTable("REQUEST_TYPES", "REQ");

        builder.ConfigureAuditableSoftDeleteEntity<RequestType, int>();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.Property(x => x.IsPaymentRequired)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(RequestType.Requests))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(RequestType.RequestFields))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(RequestType.RequiredDocuments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}