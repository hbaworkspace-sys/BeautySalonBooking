using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class BranchMemberScheduleConfiguration : IEntityTypeConfiguration<BranchMemberSchedule>
{
    public void Configure(EntityTypeBuilder<BranchMemberSchedule> builder)
    {
        builder.ToTable("BRANCH_MEMBER_SCHEDULES", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<BranchMemberSchedule, long>();

        builder.HasOne(x => x.BranchMember)
            .WithMany()
            .HasForeignKey(x => x.BranchMemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.BranchMemberId)
            .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(BranchMemberSchedule.WorkingShifts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(BranchMemberSchedule.Exceptions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(BranchMemberSchedule.TimeOffs))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}