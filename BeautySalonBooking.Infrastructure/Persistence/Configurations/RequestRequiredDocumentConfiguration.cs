using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class RequestRequiredDocumentConfiguration : IEntityTypeConfiguration<RequestRequiredDocument>
{
    public void Configure(EntityTypeBuilder<RequestRequiredDocument> builder)
    {
        builder.ToTable("REQUEST_REQUIRED_DOCUMENTS", "REQ");

        builder.ConfigureAuditableSoftDeleteEntity<RequestRequiredDocument, int>();

        builder.Property(x => x.IsRequired)
            .IsRequired();

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.HasOne(x => x.RequestType)
            .WithMany(x => x.RequiredDocuments)
            .HasForeignKey(x => x.RequestTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DocumentType)
            .WithMany(x => x.RequiredDocuments)
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.RequestTypeId,
            x.DocumentTypeId
        })
        .IsUnique();
    }
}