using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly BeautyDbContext _context;

    public BranchRepository(BeautyDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Branch>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Branches
                   .AsNoTracking()
                   .Where(x => x.IsActive && !x.IsDeleted)
                   .Include(x => x.Media)
                   .OrderBy(x => x.Title)
                   .ToListAsync(cancellationToken);
    }
}
