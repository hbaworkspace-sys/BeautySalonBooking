using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class BranchWorkingHourConfiguration : IEntityTypeConfiguration<BranchWorkingHour>
{
    public void Configure(EntityTypeBuilder<BranchWorkingHour> builder)
    {
        builder.ToTable("BRANCH_WORKING_HOURS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<BranchWorkingHour, long>();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.HasOne(x => x.BranchSchedule)
            .WithMany(x => x.WorkingHours)
            .HasForeignKey(x => x.BranchScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}