using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.ServiceAggregate.Entities;

namespace BeautySalonBooking.Domain.ServiceAggregate.Repositories;

public interface IServiceRepository : IRepository<Service, long>
{
    Task<IReadOnlyList<Service>> GetByCategoryIdAsync(
        int categoryId,
        CancellationToken cancellationToken = default);
}