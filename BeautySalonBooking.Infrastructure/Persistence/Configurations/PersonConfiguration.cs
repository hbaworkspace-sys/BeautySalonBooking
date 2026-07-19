using BeautySalonBooking.Domain.PersonAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("PERSONS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<Person, long>();


        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.FatherName)
            .HasMaxLength(50);

        builder.Property(x => x.NationalCode)
            .HasMaxLength(10);

        builder.Property(x => x.Gender)
            .IsRequired();

        builder.Property(x => x.BirthDate);

        builder.Property(x => x.BirthCertificateNumber)
            .HasMaxLength(20);

        builder.Property(x => x.BirthCertificateSerial)
            .HasMaxLength(20);


        builder.HasIndex(x => x.NationalCode)
            .IsUnique()
            .HasFilter("[NationalCode] IS NOT NULL");

        builder.HasOne(x => x.BirthRegion)
            .WithMany()
            .HasForeignKey(x => x.BirthRegionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Metadata
            .FindNavigation(nameof(Person.Users))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}