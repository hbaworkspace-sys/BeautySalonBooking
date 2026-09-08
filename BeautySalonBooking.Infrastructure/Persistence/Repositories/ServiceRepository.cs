using BeautySalonBooking.Domain.ServiceAggregate.Entities;
using BeautySalonBooking.Domain.ServiceAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;
public sealed class ServiceRepository
    : Repository<Service, long>,
      IServiceRepository
{
    private readonly BeautyDbContext _context;

    public ServiceRepository(BeautyDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Service>> GetByCategoryIdAsync(
        int categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Services
            .AsNoTracking()
            .Where(
                x =>
                    x.CategoryId == categoryId &&
                    x.IsActive &&
                    !x.IsDeleted)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
    }
}
