using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories
{
    public class BranchMemberServiceRepository : Repository<BranchMemberService, long>, IBranchMemberServiceRepository
    {
        public BranchMemberServiceRepository(BeautyDbContext context) : base(context) { }

        public async Task<List<BranchMemberService>> GetByBranchMemberIdAsync(long branchMemberId, CancellationToken ct = default)
        {
            return await _context.BranchMemberServices
                .AsNoTracking()
                .Include(x => x.BranchService)
                    .ThenInclude(x => x.Service)
                .Where(x => x.BranchMemberId == branchMemberId && !x.IsDeleted)
                .ToListAsync(ct);
        }
    }
}
