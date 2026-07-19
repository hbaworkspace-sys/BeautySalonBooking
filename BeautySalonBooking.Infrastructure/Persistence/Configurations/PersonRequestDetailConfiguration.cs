using BeautySalonBooking.Domain.RequestAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class PersonRequestDetailConfiguration : IEntityTypeConfiguration<PersonRequestDetail>
{
    public void Configure(EntityTypeBuilder<PersonRequestDetail> builder)
    {
        builder.ToTable("PERSON_REQUEST_DETAILS", "REQ");

        builder.ConfigureAuditableEntity<PersonRequestDetail, long>();

        builder.Property(x => x.Biography)
            .HasMaxLength(2000);

        builder.Property(x => x.Skills)
            .HasMaxLength(1000);

        builder.Property(x => x.Certifications)
            .HasMaxLength(1000);

        builder.Property(x => x.Instagram)
            .HasMaxLength(100);

        builder.Property(x => x.Website)
            .HasMaxLength(100);

        builder.Property(x => x.AdditionalInfo)
            .HasMaxLength(2000);

        builder.HasOne(x => x.Request)
            .WithOne(x => x.PersonRequestData)
            .HasForeignKey<PersonRequestDetail>(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}