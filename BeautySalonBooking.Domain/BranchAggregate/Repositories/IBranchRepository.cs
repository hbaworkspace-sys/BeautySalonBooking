using BeautySalonBooking.Domain.BranchAggregate.Entities;

namespace BeautySalonBooking.Domain.BranchAggregate.Repositories;

public interface IBranchRepository
{
    Task<IReadOnlyList<Branch>> GetAllAsync(
    CancellationToken cancellationToken = default);
}