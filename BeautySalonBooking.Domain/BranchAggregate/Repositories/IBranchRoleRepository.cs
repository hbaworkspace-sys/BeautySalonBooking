using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.BranchAggregate.Repositories
{
    public interface IBranchRoleRepository : IRepository<BranchRole, int>
    {
        Task<BranchRole?> GetByCodeAsync(string code, CancellationToken ct = default);
    }
}
