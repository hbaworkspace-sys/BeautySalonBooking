using BeautySalonBooking.Domain.RequestAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;

public sealed class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.ToTable("DocumentTypes", "Request");

        builder.ConfigureAuditableSoftDeleteEntity<DocumentType, int>();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(DocumentType.RequestDocuments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(DocumentType.RequiredDocuments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}