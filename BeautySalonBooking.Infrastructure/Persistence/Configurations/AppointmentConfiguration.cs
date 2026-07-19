using BeautySalonBooking.Domain.AppointmentAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("APPOINTMENTS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<Appointment, long>();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.TotalPrice)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.PaymentStatus)
            .IsRequired();

        builder.Property(x => x.Note)
            .HasMaxLength(200);

        builder.Property(x => x.CancelReason)
            .HasMaxLength(200);

        builder.HasOne(x => x.CustomerUser)
            .WithMany()
            .HasForeignKey(x => x.CustomerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BranchMemberService)
            .WithMany()
            .HasForeignKey(x => x.BranchMemberServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CancelledByUser)
            .WithMany()
            .HasForeignKey(x => x.CancelledByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.BranchMemberServiceId,
            x.Date,
            x.StartTime
        })
        .IsUnique();
    }
}