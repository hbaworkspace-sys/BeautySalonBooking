using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class BranchServiceConfiguration : IEntityTypeConfiguration<BranchService>
{
    public void Configure(EntityTypeBuilder<BranchService> builder)
    {
        builder.ToTable("BRANCH_SERVICES", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<BranchService, long>();

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.Duration)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.Property(x => x.FromDate)
            .IsRequired();

        builder.Property(x => x.ToDate)
            .IsRequired();

        builder.HasOne(x => x.Branch)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Service)
            .WithMany()
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.BranchId,
            x.ServiceId
        })
        .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(BranchService.BranchMemberServices))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}