using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USERS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<User, long>();

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(15);

        builder.HasIndex(x => x.UserName)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.AuthenticationMode)
            .IsRequired();

        builder.Property(x => x.AccessFailedCount)
            .IsRequired();

        builder.Property(x => x.LockoutEnd);

        builder.Property(x => x.LastLoginAt);

        builder.Property(x => x.LastPasswordChangedAt);

        builder.Property(x => x.TwoFactorEnabled)
            .IsRequired();

        builder.Property(x => x.LastFailedLoginAt);

        builder.Property(x => x.LastLogoutAt);

        builder.Property(x => x.Tag1)
            .HasMaxLength(100);

        builder.Property(x => x.Tag2)
            .HasMaxLength(100);

        builder.Property(x => x.Tag3)
            .HasMaxLength(100);

        builder.HasOne(x => x.Person)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Metadata
            .FindNavigation(nameof(User.UserRoles))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(User.OrganizationOwners))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}