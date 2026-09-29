using BeautySalonBooking.Domain.ServiceAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class ServiceMediaConfiguration
    : IEntityTypeConfiguration<ServiceMedia>
{
    public void Configure(EntityTypeBuilder<ServiceMedia> builder)
    {
        builder.ToTable("SERVICE_MEDIAS", "GT");

        builder.ConfigureAuditableEntity<ServiceMedia, long>();

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

        builder.HasOne(x => x.Service)
            .WithMany(x => x.Media)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}