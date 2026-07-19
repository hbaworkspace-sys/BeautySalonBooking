using BeautySalonBooking.Domain.Identity.PermissionAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("PERMISSIONS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<Permission, int>();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(Permission.RolePermissions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}