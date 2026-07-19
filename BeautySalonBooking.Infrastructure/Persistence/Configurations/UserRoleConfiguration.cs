using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("USER_ROLES", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<UserRole, int>();

        builder.Property(x => x.IsPrimary)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.RoleId
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.UserId,
            x.IsPrimary
        })
        .HasFilter("[IsPrimary] = 1")
        .IsUnique();
    }
}