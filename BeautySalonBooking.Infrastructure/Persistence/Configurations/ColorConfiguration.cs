using BeautySalonBooking.Domain.CommonAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("COLORS", "BT");

        builder.ConfigureAuditableSoftDeleteEntity<Color, int>();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.HexCode)
            .HasMaxLength(7);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}