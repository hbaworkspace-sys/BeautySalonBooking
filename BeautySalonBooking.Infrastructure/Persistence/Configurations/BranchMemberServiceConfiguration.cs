using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class BranchMemberServiceConfiguration : IEntityTypeConfiguration<BranchMemberService>
{
    public void Configure(EntityTypeBuilder<BranchMemberService> builder)
    {
        builder.ToTable("BRANCH_MEMBER_SERVICES", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<BranchMemberService, long>();

        builder.Property(x => x.Price);

        builder.Property(x => x.Duration);

        builder.HasOne(x => x.BranchMember)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.BranchMemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BranchService)
            .WithMany()
            .HasForeignKey(x => x.BranchServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.BranchMemberId,
            x.BranchServiceId
        })
        .IsUnique();
    }
}