using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;
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
          .HasMaxLength(200)
          .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(100)
            .IsRequired();


        builder.Property(x => x.Type);

        builder.Property(x => x.Description)
                .HasMaxLength(200);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);





    }
}