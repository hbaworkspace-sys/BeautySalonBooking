using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.BranchAggregate.ReadModels;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public sealed class BranchServiceRepository
    : Repository<BranchService, long>,
      IBranchServiceRepository
{
    private readonly BeautyDbContext _context;
    private readonly IBranchMemberRepository _branchMemberRepository;

    public BranchServiceRepository(BeautyDbContext context, IBranchMemberRepository branchMemberRepository)
        : base(context)
    {
        _context = context;
        _branchMemberRepository = branchMemberRepository;
    }

    public async Task<IReadOnlyList<BranchServiceOrganizationReadModel>>
        GetBranchesByServiceIdAsync(
            long serviceId,
            CancellationToken cancellationToken = default)
    {
        return await _context.BranchServices
            .AsNoTracking()
            .Where(x =>
                x.ServiceId == serviceId &&
                x.IsActive &&
                !x.IsDeleted &&
                x.Branch.IsActive &&
                !x.Branch.IsDeleted &&
                x.Branch.Organization.IsActive &&
                !x.Branch.Organization.IsDeleted
                && _context.BranchMemberServices.Any(bms =>
                bms.BranchServiceId == x.Id &&
                bms.IsActive &&
                !bms.IsDeleted &&
                bms.BranchMember.IsActive &&
                !bms.BranchMember.IsDeleted &&
                     _context.BranchMemberServices.Any(bms =>
                        bms.BranchServiceId == x.Id &&
                        bms.IsActive &&
                        !bms.IsDeleted &&
                        bms.BranchMember.IsActive &&
                        !bms.BranchMember.IsDeleted &&
                        _context.BranchMemberSchedules.Any(schedule =>
                            schedule.BranchMemberId == bms.BranchMemberId &&
                            schedule.IsActive &&
                            !schedule.IsDeleted &&
                            _context.WorkingShifts.Any(shift =>
                                shift.BranchMemberScheduleId == schedule.Id &&
                                shift.IsActive &&
                                !shift.IsDeleted)))))
            .OrderBy(x => x.DisplayOrder)
            .Select(x => new BranchServiceOrganizationReadModel
            {
                BranchServiceId = x.Id,

                BranchId = x.BranchId,
                BranchTitle = x.Branch.Title,
                BranchDescription = x.Branch.Description,

                OrganizationId = x.Branch.OrganizationId,
                OrganizationTitle = x.Branch.Organization.Title,
                OrganizationType = x.Branch.Organization.Type,

                Price = x.Price,
                Duration = x.Duration,
                DisplayOrder = x.DisplayOrder,
                Media = x.Branch.Media
                    .OrderBy(m => m.DisplayOrder)
                    .ToList()
            })
            .ToListAsync(cancellationToken);
    }


    // 👇 متد جدید: لیست با فیلتر و صفحه‌بندی
    public async Task<(IReadOnlyList<BranchService> Items, int TotalCount)> GetFilteredAsync(long userId,
        long? branchId,
        long? serviceId,
        decimal? minPrice,
        decimal? maxPrice,
        string? searchTerm,
        bool? isActive,
        int pageNumber,
        int pageSize,
        string? sortBy,
        bool sortDescending,
        CancellationToken ct = default)
    {


        var banches = await _branchMemberRepository.GetBranchIdsByUserIdAsync(userId, ct);
        var query = _context.BranchServices
            .AsNoTracking()
            .Include(x => x.Branch)
            .Include(x => x.Service)
            .Where(x => !x.IsDeleted && banches.Contains(x.BranchId));

        // اعمال فیلترها
        if (branchId.HasValue)
            query = query.Where(x => x.BranchId == branchId.Value);

        if (serviceId.HasValue)
            query = query.Where(x => x.ServiceId == serviceId.Value);

        if (minPrice.HasValue)
            query = query.Where(x => x.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(x => x.Price <= maxPrice.Value);

        if (!string.IsNullOrWhiteSpace(searchTerm))
            query = query.Where(x => x.Description != null && x.Description.Contains(searchTerm));

        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);

        // تعداد کل
        var totalCount = await query.CountAsync(ct);

        // مرتب‌سازی
        query = sortBy?.ToLower() switch
        {
            "price" => sortDescending ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price),
            "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
            "duration" => sortDescending ? query.OrderByDescending(x => x.Duration) : query.OrderBy(x => x.Duration),
            _ => sortDescending ? query.OrderByDescending(x => x.DisplayOrder) : query.OrderBy(x => x.DisplayOrder)
        };

        // صفحه‌بندی
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }

    // 👇 متد جدید: گرفتن با Include (برای DTO)
    public async Task<BranchService?> GetWithDetailsAsync(long id, CancellationToken ct = default)
    {
        return await _context.BranchServices
            .AsNoTracking()
            .Include(x => x.Branch)
            .Include(x => x.Service)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }


}