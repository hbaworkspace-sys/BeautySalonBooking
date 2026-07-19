using BeautySalonBooking.Domain.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;

public static class AuditableSoftDeleteEntityConfiguration
{
    public static void ConfigureAuditableSoftDeleteEntity<TEntity, TId>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableSoftDeleteEntity<TId>
    {
        builder.ConfigureAuditableEntity<TEntity, TId>();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.DeletedAt);

        builder.Property(x => x.DeletedBy);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
