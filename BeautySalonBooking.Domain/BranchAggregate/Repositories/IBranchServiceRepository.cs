using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.BranchAggregate.ReadModels;

namespace BeautySalonBooking.Domain.BranchAggregate.Repositories;

public interface IBranchServiceRepository
    : IRepository<BranchService, long>
{
    Task<IReadOnlyList<BranchServiceOrganizationReadModel>>
        GetBranchesByServiceIdAsync(
            long serviceId,
            CancellationToken cancellationToken = default);
}