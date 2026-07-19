using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class RequestDetailConfiguration : IEntityTypeConfiguration<RequestDetail>
{
    public void Configure(EntityTypeBuilder<RequestDetail> builder)
    {
        builder.ToTable("REQUEST_DETAILS", "REQ");

        builder.ConfigureAuditableEntity<RequestDetail, long>();

        builder.Property(x => x.Value)
            .HasMaxLength(200);

        builder.HasOne(x => x.Request)
            .WithMany()
            .HasForeignKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.RequestField)
            .WithMany(x => x.RequestDetails)
            .HasForeignKey(x => x.RequestFieldId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}