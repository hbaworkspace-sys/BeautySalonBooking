using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.BranchAggregate.ReadModels;
using BeautySalonBooking.Domain.BranchAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public sealed class BranchServiceRepository
    : Repository<BranchService, long>,
      IBranchServiceRepository
{
    private readonly BeautyDbContext _context;

    public BranchServiceRepository(BeautyDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<BranchServiceOrganizationReadModel>>
        GetBranchesByServiceIdAsync(
            long serviceId,
            CancellationToken cancellationToken = default)
    {
        return await _context.BranchServices
            .AsNoTracking()
            .Where(x =>
                x.ServiceId == serviceId &&
                x.IsActive &&
                !x.IsDeleted &&
                x.Branch.IsActive &&
                !x.Branch.IsDeleted &&
                x.Branch.Organization.IsActive &&
                !x.Branch.Organization.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .Select(x => new BranchServiceOrganizationReadModel
            {
                BranchServiceId = x.Id,

                BranchId = x.BranchId,
                BranchTitle = x.Branch.Title,
                BranchDescription = x.Branch.Description,

                OrganizationId = x.Branch.OrganizationId,
                OrganizationTitle = x.Branch.Organization.Title,
                OrganizationType = x.Branch.Organization.Type,

                Price = x.Price,
                Duration = x.Duration,
                DisplayOrder = x.DisplayOrder
            })
            .ToListAsync(cancellationToken);
    }
}