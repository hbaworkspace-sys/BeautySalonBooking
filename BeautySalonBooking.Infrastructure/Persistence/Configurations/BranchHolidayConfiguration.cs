using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class BranchHolidayConfiguration : IEntityTypeConfiguration<BranchHoliday>
{
    public void Configure(EntityTypeBuilder<BranchHoliday> builder)
    {
        builder.ToTable("BRANCH_HOLIDAYS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<BranchHoliday, long>();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.Property(x => x.IsNationalHoliday)
            .IsRequired();

        builder.HasOne(x => x.Branch)
            .WithMany()
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.BranchId,
            x.Date
        })
        .IsUnique();
    }
}