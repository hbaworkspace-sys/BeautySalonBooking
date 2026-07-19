using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class OrganizationDetailConfiguration : IEntityTypeConfiguration<OrganizationDetail>
{
    public void Configure(EntityTypeBuilder<OrganizationDetail> builder)
    {
        builder.ToTable("ORGANIZATION_DETAILS", "GT");


        builder.ConfigureAuditableEntity<OrganizationDetail, long>();


        builder.Property(x => x.Description)
            .HasMaxLength(2000);


        builder.Property(x => x.Website)
            .HasMaxLength(100);


        builder.Property(x => x.Instagram)
            .HasMaxLength(100);


        builder.Property(x => x.Email)
            .HasMaxLength(100);



        builder.HasOne(x => x.Organization)
            .WithOne()
            .HasForeignKey<OrganizationDetail>(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}