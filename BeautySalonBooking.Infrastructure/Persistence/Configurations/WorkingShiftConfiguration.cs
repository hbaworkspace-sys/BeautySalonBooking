using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class WorkingShiftConfiguration : IEntityTypeConfiguration<WorkingShift>
{
    public void Configure(EntityTypeBuilder<WorkingShift> builder)
    {
        builder.ToTable("WORKING_SHIFTS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<WorkingShift, long>();

        builder.Property(x => x.DayOfWeek)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.HasOne(x => x.BranchMemberSchedule)
            .WithMany(x => x.WorkingShifts)
            .HasForeignKey(x => x.BranchMemberScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.BranchMemberScheduleId,
            x.DayOfWeek,
            x.StartTime,
            x.EndTime
        })
        .IsUnique();
    }
}