using BeautySalonBooking.Domain.ServiceAggregate.Entities;
using BeautySalonBooking.Domain.ServiceAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;
public sealed class ServiceRepository
    : Repository<Service, long>,
      IServiceRepository
{
    private readonly BeautyDbContext _context;

    public ServiceRepository(BeautyDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Service>> GetByCategoryIdAsync(
        int categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Services
            .AsNoTracking()
            .Include(x => x.Media)
            .Where(x =>
                x.CategoryId == categoryId &&
                x.IsActive &&
                !x.IsDeleted &&
                _context.BranchServices.Any(bs =>
                    bs.ServiceId == x.Id &&
                    bs.IsActive &&
                    !bs.IsDeleted &&
                    bs.Branch.IsActive &&
                    !bs.Branch.IsDeleted &&
                    _context.BranchMemberServices.Any(bms =>
                        bms.BranchServiceId == bs.Id &&
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
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
    }
}