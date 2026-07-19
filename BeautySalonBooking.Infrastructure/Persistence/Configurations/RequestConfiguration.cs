using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable("REQUESTS", "REQ");

        builder.ConfigureAuditableEntity<Request, long>();

        builder.Property(x => x.TrackingCode)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CurrentStep)
            .IsRequired();

        builder.Property(x => x.SubmittedAt)
            .IsRequired();

        builder.Property(x => x.ReviewDescription)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.TrackingCode)
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RequestType)
            .WithMany(x => x.Requests)
            .HasForeignKey(x => x.RequestTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Reviewer)
            .WithMany()
            .HasForeignKey(x => x.ReviewedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.OrganizationRequestData)
            .WithOne(x => x.Request)
            .HasForeignKey<OrganizationRequestDetail>(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PersonRequestData)
            .WithOne(x => x.Request)
            .HasForeignKey<PersonRequestDetail>(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(Request.Documents))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Request.Histories))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}