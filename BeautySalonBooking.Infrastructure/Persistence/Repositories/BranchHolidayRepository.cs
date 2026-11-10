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
    public class BranchHolidayRepository : Repository<BranchHoliday, long>, IBranchHolidayRepository
    {
        public BranchHolidayRepository(BeautyDbContext context) : base(context) { }

        public async Task<List<BranchHoliday>> GetByBranchIdAsync(long branchId, CancellationToken ct = default)
        {
            return await _context.BranchHolidays
                .AsNoTracking()
                .Where(x => x.BranchId == branchId && !x.IsDeleted)
                .OrderBy(x => x.Date)
                .ToListAsync(ct);
        }
    }
}
