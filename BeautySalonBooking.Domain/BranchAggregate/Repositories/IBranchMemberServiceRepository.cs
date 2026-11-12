using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.BranchAggregate.Repositories
{
    // IBranchMemberServiceRepository.cs (جدید)
    public interface IBranchMemberServiceRepository : IRepository<BranchMemberService, long>
    {
        Task<List<BranchMemberService>> GetByBranchMemberIdAsync(long branchMemberId, CancellationToken ct = default);
    }
}
