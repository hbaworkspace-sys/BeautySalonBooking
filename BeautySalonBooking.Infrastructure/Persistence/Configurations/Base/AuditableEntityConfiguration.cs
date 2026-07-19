using BeautySalonBooking.Domain.Base.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;

public static class AuditableEntityConfiguration
{
    public static void ConfigureAuditableEntity<TEntity, TId>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableEntity<TId>
    {
        builder.ConfigureBaseEntity<TEntity, TId>();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy);

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.UpdatedBy);
    }
}