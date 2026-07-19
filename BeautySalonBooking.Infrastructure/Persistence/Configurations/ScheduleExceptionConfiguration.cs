using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class ScheduleExceptionConfiguration : IEntityTypeConfiguration<ScheduleException>
{
    public void Configure(EntityTypeBuilder<ScheduleException> builder)
    {
        builder.ToTable("SCHEDULE_EXCEPTIONS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<ScheduleException, long>();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.IsWorkingDay)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.HasOne(x => x.BranchMemberSchedule)
            .WithMany(x => x.Exceptions)
            .HasForeignKey(x => x.BranchMemberScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.BranchMemberScheduleId,
            x.Date
        })
        .IsUnique();
    }
}