using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class RequestHistoryConfiguration : IEntityTypeConfiguration<RequestHistory>
{
    public void Configure(EntityTypeBuilder<RequestHistory> builder)
    {
        builder.ToTable("REQUEST_HISTORIES", "REQ");

        builder.ConfigureAuditableEntity<RequestHistory, long>();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Action)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.HasOne(x => x.Request)
            .WithMany(x => x.Histories)
            .HasForeignKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ChangedByUser)
            .WithMany()
            .HasForeignKey(x => x.ChangedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}