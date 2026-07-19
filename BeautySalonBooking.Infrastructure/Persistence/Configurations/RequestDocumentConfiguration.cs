using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class RequestDocumentConfiguration : IEntityTypeConfiguration<RequestDocument>
{
    public void Configure(EntityTypeBuilder<RequestDocument> builder)
    {
        builder.ToTable("REQUEST_DOCUMENTS", "REQ");

        builder.ConfigureAuditableEntity<RequestDocument, long>();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.ReviewDescription)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Request)
            .WithMany(x => x.Documents)
            .HasForeignKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.DocumentType)
            .WithMany(x => x.RequestDocuments)
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ReviewedByUser)
            .WithMany()
            .HasForeignKey(x => x.ReviewedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}