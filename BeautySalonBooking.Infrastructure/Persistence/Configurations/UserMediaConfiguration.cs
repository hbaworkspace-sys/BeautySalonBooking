using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class UserMediaConfiguration : IEntityTypeConfiguration<UserMedia>
{
    public void Configure(EntityTypeBuilder<UserMedia> builder)
    {
        builder.ToTable("USER_MEDIAS", "GT");

        builder.ConfigureAuditableEntity<UserMedia, long>();

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

        builder.HasOne(x => x.User)
            .WithMany(x => x.Media)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}