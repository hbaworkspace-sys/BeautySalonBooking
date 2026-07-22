using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Enums;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly BeautyDbContext _context;

    public RoleRepository(BeautyDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Role?> GetByCodeAsync(RoleCode roleCode, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(x => x.Code == roleCode, cancellationToken);
    }

    public async Task AddAsync(Role role, CancellationToken cancellationToken)
    {
        await _context.Roles.AddAsync(role, cancellationToken);
    }
}
