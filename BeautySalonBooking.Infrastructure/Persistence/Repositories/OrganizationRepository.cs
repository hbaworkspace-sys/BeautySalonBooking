using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public sealed class OrganizationRepository : IOrganizationRepository
{
    private readonly BeautyDbContext _context;

    public OrganizationRepository(BeautyDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Organization>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Organizations
            .AsNoTracking()
            .Where(x => x.IsActive && !x.IsDeleted)
            .Include(x => x.Media)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
    }
}