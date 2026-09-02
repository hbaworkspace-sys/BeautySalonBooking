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
}