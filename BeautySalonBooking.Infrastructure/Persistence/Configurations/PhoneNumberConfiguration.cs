using BeautySalonBooking.Domain.ContactAggregate.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public static class PhoneNumberConfiguration
{
    public static void Configure<TOwner>(
            OwnedNavigationBuilder<TOwner, PhoneNumber> builder)
            where TOwner : class
    {
        builder.WithOwner()
               .HasForeignKey("UserId");
        builder.HasKey("UserId", nameof(PhoneNumber.Number));

        builder.Property(x => x.Number)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Type)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(x => x.IsDefault);
        builder.Property(x => x.IsVerified);
    }
}