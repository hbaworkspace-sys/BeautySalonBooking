using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.CategoryAggregate.Entities;

namespace BeautySalonBooking.Domain.CategoryAggregate.Repositories;

public interface ICategoryRepository : IRepository<Category, int>
{
    Task<Category?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Category>> GetChildrenAsync(
        int parentId,
        CancellationToken cancellationToken = default);

    // 👇 متدهای جدید
    Task<IReadOnlyList<Category>> GetRootsAsync(int? type, CancellationToken ct = default);
    Task<bool> HasChildrenAsync(int categoryId, CancellationToken ct = default);
    Task<bool> HasServicesAsync(int categoryId, CancellationToken ct = default);
    Task<bool> CodeExistsAsync(string code, int? excludeId = null, CancellationToken ct = default);
    Task<Category?> GetWithMediaAsync(int id, CancellationToken ct = default);
}