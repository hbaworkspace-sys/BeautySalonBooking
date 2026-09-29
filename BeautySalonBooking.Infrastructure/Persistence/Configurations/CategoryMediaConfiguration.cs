using BeautySalonBooking.Domain.CategoryAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class CategoryMediaConfiguration
    : IEntityTypeConfiguration<CategoryMedia>
{
    public void Configure(EntityTypeBuilder<CategoryMedia> builder)
    {
        builder.ToTable("CATEGORY_MEDIAS", "BT");

        builder.ConfigureAuditableEntity<CategoryMedia, long>();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Media)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}