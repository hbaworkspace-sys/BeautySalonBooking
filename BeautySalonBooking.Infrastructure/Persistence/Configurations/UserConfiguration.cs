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
               .HasMaxLength(15);

        builder.HasIndex(x => x.UserName)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
             .HasMaxLength(200);

        builder.Property(x => x.AuthenticationMode)
            .IsRequired();

        builder.Property(x => x.AccessFailedCount);

        builder.Property(x => x.LockoutEnd);

        builder.Property(x => x.LastLoginAt);

        builder.Property(x => x.LastPasswordChangedAt);

        builder.Property(x => x.TwoFactorEnabled);

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
            .FindNavigation(nameof(User.UserRole))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(User.OrganizationOwners))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(User.PhoneNumbers))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(x => x.PhoneNumbers, phone =>
        {
            phone.ToTable("USER_PHONE_NUMBERS", "GT");

            PhoneNumberConfiguration.Configure(phone);
        });
    }
}