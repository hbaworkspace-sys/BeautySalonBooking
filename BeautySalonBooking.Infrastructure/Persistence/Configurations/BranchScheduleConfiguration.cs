using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class BranchScheduleConfiguration : IEntityTypeConfiguration<BranchSchedule>
{
    public void Configure(EntityTypeBuilder<BranchSchedule> builder)
    {
        builder.ToTable("BRANCH_SCHEDULES", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<BranchSchedule, long>();

        builder.Property(x => x.DayOfWeek)
            .IsRequired();

        builder.Property(x => x.IsWorkingDay)
            .IsRequired();

        builder.HasOne(x => x.Branch)
            .WithMany()
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.BranchId,
            x.DayOfWeek
        })
        .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(BranchSchedule.WorkingHours))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}