using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using BeautySalonBooking.Domain.SchedulingAggregate.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories
{
    // BranchScheduleRepository.cs
    public class BranchScheduleRepository : Repository<BranchSchedule, long>, IBranchScheduleRepository
    {
        public BranchScheduleRepository(BeautyDbContext context) : base(context) { }

        public async Task<List<BranchSchedule>> GetByBranchIdAsync(long branchId, CancellationToken ct = default)
        {
            return await _context.BranchSchedules
                .AsNoTracking()
                .Include(x => x.WorkingHours)
                .Where(x => x.BranchId == branchId && !x.IsDeleted)
                .OrderBy(x => x.DayOfWeek)
                .ToListAsync(ct);
        }
    }
}
