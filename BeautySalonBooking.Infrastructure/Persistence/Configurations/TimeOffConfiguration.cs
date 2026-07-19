using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class TimeOffConfiguration : IEntityTypeConfiguration<TimeOff>
{
    public void Configure(EntityTypeBuilder<TimeOff> builder)
    {
        builder.ToTable("TIMEOFFS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<TimeOff, long>();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(200);

        builder.HasOne(x => x.BranchMemberSchedule)
            .WithMany(x => x.TimeOffs)
            .HasForeignKey(x => x.BranchMemberScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}