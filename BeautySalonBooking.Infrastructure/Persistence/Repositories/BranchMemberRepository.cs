using BeautySalonBooking.Domain.BranchAggregate.Constants;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.BranchAggregate.ReadModels;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public sealed class BranchMemberRepository
    : Repository<BranchMember, long>,
      IBranchMemberRepository
{
    private readonly BeautyDbContext _context;

    public BranchMemberRepository(BeautyDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<BranchMemberServiceReadModel>>
        GetByBranchAndServiceAsync(
            long branchId,
            long serviceId,
            CancellationToken cancellationToken = default)
    {
        return await _context.BranchMembers
            .AsNoTracking()
            .Where(member =>
                member.BranchId == branchId &&
                member.IsActive &&
                !member.IsDeleted &&
                member.BranchRole.IsActive &&
                !member.BranchRole.IsDeleted &&
                member.BranchRole.Code == BranchRoleCodes.Stylist &&
                member.Services.Any(memberService =>
                    memberService.IsActive &&
                    !memberService.IsDeleted &&
                    memberService.BranchService.BranchId == branchId &&
                    memberService.BranchService.ServiceId == serviceId &&
                    memberService.BranchService.IsActive &&
                    !memberService.BranchService.IsDeleted))
            .OrderBy(member => member.Person.FirstName)
            .ThenBy(member => member.Person.LastName)
.Select(member => new BranchMemberServiceReadModel
{
    BranchMemberId = member.Id,

    BranchMemberServiceId = member.Services
        .Where(memberService =>
            memberService.IsActive &&
            !memberService.IsDeleted &&
            memberService.BranchService.BranchId == branchId &&
            memberService.BranchService.ServiceId == serviceId &&
            memberService.BranchService.IsActive &&
            !memberService.BranchService.IsDeleted)
        .Select(memberService => memberService.Id)
        .First(),

    PersonId = member.PersonId,

    FirstName = member.Person.FirstName,

    LastName = member.Person.LastName,

    Price = member.Services
        .Where(memberService =>
            memberService.IsActive &&
            !memberService.IsDeleted &&
            memberService.BranchService.BranchId == branchId &&
            memberService.BranchService.ServiceId == serviceId &&
            memberService.BranchService.IsActive &&
            !memberService.BranchService.IsDeleted)
        .Select(memberService =>
            memberService.Price ??
            memberService.BranchService.Price)
        .FirstOrDefault(),

    Duration = member.Services
        .Where(memberService =>
            memberService.IsActive &&
            !memberService.IsDeleted &&
            memberService.BranchService.BranchId == branchId &&
            memberService.BranchService.ServiceId == serviceId &&
            memberService.BranchService.IsActive &&
            !memberService.BranchService.IsDeleted)
        .Select(memberService =>
            memberService.Duration ??
            memberService.BranchService.Duration)
        .FirstOrDefault()
})
.ToListAsync(cancellationToken);
    }
}