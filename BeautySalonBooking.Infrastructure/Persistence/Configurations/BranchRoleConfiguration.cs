using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class BranchRoleConfiguration : IEntityTypeConfiguration<BranchRole>
{
    public void Configure(EntityTypeBuilder<BranchRole> builder)
    {
        builder.ToTable("BRANCH_ROLES", "BT");

        builder.ConfigureAuditableSoftDeleteEntity<BranchRole, int>();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.HasIndex(x => x.Code)
             .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(BranchRole.Members))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}