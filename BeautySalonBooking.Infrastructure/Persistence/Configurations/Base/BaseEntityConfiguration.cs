using BeautySalonBooking.Domain.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;

public static class BaseEntityConfiguration
{
    public static void ConfigureBaseEntity<TEntity, TId>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntity<TId>
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
           .UseIdentityColumn();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }
}
