using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalonBooking.Infrastructure.Persistence.Configurations;

public sealed class BranchMemberConfiguration : IEntityTypeConfiguration<BranchMember>
{
    public void Configure(EntityTypeBuilder<BranchMember> builder)
    {
        builder.ToTable("BRANCH_MEMBERS", "GT");

        builder.ConfigureAuditableSoftDeleteEntity<BranchMember, long>();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate);

        builder.HasOne(x => x.Branch)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Person)
            .WithMany()
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BranchRole)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.BranchRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Metadata
            .FindNavigation(nameof(BranchMember.Services))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}