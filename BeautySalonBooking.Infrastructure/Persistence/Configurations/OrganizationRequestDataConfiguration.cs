using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class OrganizationRequestDetailConfiguration : IEntityTypeConfiguration<OrganizationRequestDetail>
{
    public void Configure(EntityTypeBuilder<OrganizationRequestDetail> builder)
    {
        builder.ToTable("ORGANIZATION_REQUEST_DETAILS", "REQ");

        builder.ConfigureAuditableEntity<OrganizationRequestDetail, long>();

        builder.Property(x => x.OrganizationName)
            .HasMaxLength(80);

        builder.Property(x => x.OrganizationDescription)
            .HasMaxLength(1000);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(10);

        builder.Property(x => x.MobileNumber)
            .HasMaxLength(10);

        builder.Property(x => x.Website)
            .HasMaxLength(100);

        builder.Property(x => x.Instagram)
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .HasMaxLength(100);

        builder.Property(x => x.Whatsapp)
            .HasMaxLength(100);

        builder.Property(x => x.BranchName)
            .HasMaxLength(80);

        builder.Property(x => x.BranchDescription)
            .HasMaxLength(1000);

        builder.Property(x => x.Address)
            .HasMaxLength(200);

        builder.Property(x => x.PostalCode)
            .HasMaxLength(10);

        builder.Property(x => x.LicenseNumber)
            .HasMaxLength(150);

        builder.Property(x => x.Slogan)
            .HasMaxLength(300);

        builder.Property(x => x.AdditionalInfo)
            .HasMaxLength(2000);

        builder.HasOne(x => x.Request)
            .WithOne(x => x.OrganizationRequestData)
            .HasForeignKey<OrganizationRequestDetail>(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Region)
            .WithMany()
            .HasForeignKey(x => x.RegionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}