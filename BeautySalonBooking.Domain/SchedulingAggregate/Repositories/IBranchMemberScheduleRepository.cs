using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.SchedulingAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Repositories
{
    public interface IBranchMemberScheduleRepository : IRepository<BranchMemberSchedule, long>
    {
        Task<BranchMemberSchedule?> GetWithDetailsAsync(long id, CancellationToken ct = default);
        Task<List<BranchMemberSchedule>> GetByBranchMemberIdAsync(long branchMemberId, CancellationToken ct = default);
    }
}
