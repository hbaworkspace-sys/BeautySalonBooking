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
    public class BranchRoleRepository : Repository<BranchRole, int>, IBranchRoleRepository
    {
        public BranchRoleRepository(BeautyDbContext context) : base(context) { }

        public async Task<BranchRole?> GetByCodeAsync(string code, CancellationToken ct = default)
        {
            return await _context.BranchRoles
                .FirstOrDefaultAsync(x => x.Code == code && !x.IsDeleted, ct);
        }
    }
}
