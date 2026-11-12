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
                // member.BranchRole.Code == BranchRoleCodes.Stylist &&
                member.Services.Any(memberService =>
                    memberService.IsActive &&
                    !memberService.IsDeleted &&
                    memberService.BranchService.BranchId == branchId &&
                    memberService.BranchService.ServiceId == serviceId &&
                    memberService.BranchService.IsActive &&
                    !memberService.BranchService.IsDeleted) &&
                     _context.BranchMemberSchedules.Any(schedule =>
                    schedule.BranchMemberId == member.Id &&
                    schedule.IsActive &&
                    !schedule.IsDeleted &&
                    _context.WorkingShifts.Any(shift =>
                        shift.BranchMemberScheduleId == schedule.Id &&
                        shift.IsActive &&
                        !shift.IsDeleted)))
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

    Medias = member.Person.Users
    .SelectMany(user => user.Media).ToList(),

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

    public async Task<List<long>> GetBranchIdsByUserIdAsync(long userId, CancellationToken ct = default)
    {
        // 1. پیدا کردن PersonId مربوط به این User
        var personId = await _context.Users
            .AsNoTracking()
            .Where(x => x.Id == userId)
            .Select(x => x.PersonId)
            .FirstOrDefaultAsync(ct);

        if (personId == 0)
            return new List<long>();

        // 2. پیدا کردن شعبه‌هایی که این Person در آن‌ها عضو است
        return await _context.BranchMembers
            .AsNoTracking()
            .Where(x => x.PersonId == personId && !x.IsDeleted)
            .Select(x => x.BranchId)
            .Distinct()
            .ToListAsync(ct);
    }
    public async Task<BranchMember?> GetWithDetailsAsync(long id, CancellationToken ct = default)
    {
        return await _context.BranchMembers
            .AsNoTracking()
            .Include(x => x.Person)
            .Include(x => x.Branch)
            .Include(x => x.BranchRole)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }


    public async Task<(List<BranchMember> Items, int TotalCount)> GetFilteredAsync(
        long? branchId,
        int? branchRoleId,
        string? searchTerm,
        bool? isActive,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        var query = _context.BranchMembers
            .AsNoTracking()
            .Include(x => x.Person)
                .ThenInclude(x => x.Users)
                    .ThenInclude(x => x.PhoneNumbers)
            .Include(x => x.Branch)
            .Include(x => x.BranchRole)
            .Include(x => x.Services)
                .ThenInclude(x => x.BranchService)
                    .ThenInclude(x => x.Service)
            .Where(x => !x.IsDeleted);

        if (branchId.HasValue)
            query = query.Where(x => x.BranchId == branchId.Value);

        if (branchRoleId.HasValue)
            query = query.Where(x => x.BranchRoleId == branchRoleId.Value);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            query = query.Where(x =>
                x.Person.FirstName.Contains(term) ||
                x.Person.LastName.Contains(term));
        }

        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }
}