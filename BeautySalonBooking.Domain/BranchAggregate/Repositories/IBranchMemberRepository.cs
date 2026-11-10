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
    Task<List<long>> GetBranchIdsByUserIdAsync(long userId, CancellationToken ct = default);
    Task<BranchMember?> GetWithDetailsAsync(long id, CancellationToken ct = default);

    Task<(List<BranchMember> Items, int TotalCount)> GetFilteredAsync(
        long? branchId,
        int? branchRoleId,
        string? searchTerm,
        bool? isActive,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}