using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class OrganizationMediaConfiguration : IEntityTypeConfiguration<OrganizationMedia>
{
    public void Configure(EntityTypeBuilder<OrganizationMedia> builder)
    {
        builder.ToTable("ORGANIZATION_MEDIAS", "GT");

        builder.ConfigureAuditableEntity<OrganizationMedia, long>();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.Media)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}