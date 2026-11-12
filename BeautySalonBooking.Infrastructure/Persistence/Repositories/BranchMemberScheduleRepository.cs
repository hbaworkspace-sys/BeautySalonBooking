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
    public class BranchMemberScheduleRepository : Repository<BranchMemberSchedule, long>, IBranchMemberScheduleRepository
    {
        public BranchMemberScheduleRepository(BeautyDbContext context) : base(context) { }

        public async Task<BranchMemberSchedule?> GetWithDetailsAsync(long id, CancellationToken ct = default)
        {
            return await _context.BranchMemberSchedules
                .AsNoTracking()
                .Include(x => x.WorkingShifts)
                .Include(x => x.Exceptions)
                .Include(x => x.TimeOffs)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
        }
        public async Task<List<BranchMemberSchedule>> GetByBranchMemberIdAsync(long branchMemberId, CancellationToken ct = default)
        {
            return await _context.BranchMemberSchedules
                .AsNoTracking()
                .Include(x => x.WorkingShifts)
                .Include(x => x.Exceptions)
                .Include(x => x.TimeOffs)
                .Where(x => x.BranchMemberId == branchMemberId && !x.IsDeleted)
                .ToListAsync(ct);
        }
    }


}
