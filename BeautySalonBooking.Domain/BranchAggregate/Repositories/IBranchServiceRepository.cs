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


    // 👇 متد جدید برای لیست با فیلتر
    Task<(IReadOnlyList<BranchService> Items, int TotalCount)> GetFilteredAsync(long userId,
        long? branchId,
        long? serviceId,
        decimal? minPrice,
        decimal? maxPrice,
        string? searchTerm,
        bool? isActive,
        int pageNumber,
        int pageSize,
        string? sortBy,
        bool sortDescending,
        CancellationToken ct = default);

    // 👇 متد جدید برای گرفتن با Include (برای DTO)
    Task<BranchService?> GetWithDetailsAsync(long id, CancellationToken ct = default);
}