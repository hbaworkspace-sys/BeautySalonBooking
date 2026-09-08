using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.BranchAggregate.ReadModels;

namespace BeautySalonBooking.Domain.BranchAggregate.Repositories;

public interface IBranchMemberRepository
    : IRepository<BranchMember, long>
{
    Task<IReadOnlyList<BranchMemberServiceReadModel>>
        GetByBranchAndServiceAsync(
            long branchId,
            long serviceId,
            CancellationToken cancellationToken = default);
}