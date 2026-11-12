using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;


public sealed class OrganizationRepository : Repository<Organization, long>, IOrganizationRepository
{
    public OrganizationRepository(BeautyDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Organization>> GetAllWithMediaAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Organizations
            .AsNoTracking()
            .Where(x => x.IsActive && !x.IsDeleted)
            .Include(x => x.Media)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
    }
    public async Task<Organization?> GetFullDetailAsync(long id, CancellationToken ct = default)
    {
        return await _context.Organizations
            .AsNoTracking()
            .Include(x => x.Detail)
            .Include(x => x.Owners)
                .ThenInclude(x => x.OwnerUser)
                    .ThenInclude(x => x.Person)
                     .Include(x => x.Media)
            .Include(x => x.Branches)
                .ThenInclude(x => x.Members)
                    .ThenInclude(x => x.Person)
            //   .ThenInclude(x => x.PhoneNumbers)
            .Include(x => x.Branches)
                .ThenInclude(x => x.Members)
                    .ThenInclude(x => x.BranchRole)
            .Include(x => x.Branches)
                .ThenInclude(x => x.Members)
                    .ThenInclude(x => x.Services)
            .Include(x => x.Branches)
                .ThenInclude(x => x.Services)
            .Include(x => x.Branches)
                .ThenInclude(x => x.Media)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}