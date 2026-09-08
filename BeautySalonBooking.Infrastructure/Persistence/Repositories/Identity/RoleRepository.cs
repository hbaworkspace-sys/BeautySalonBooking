using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Enums;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Repositories;
using BeautySalonBooking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;


public class RoleRepository
    : Repository<Role, int>, IRoleRepository
{


    private readonly BeautyDbContext _context;


    public RoleRepository(
        BeautyDbContext context)
        : base(context)
    {
        _context = context;
    }


    public async Task<Role?> GetRoleWithPermissionsAsync(
    int roleId,
    CancellationToken cancellationToken = default)
    {

        return await _context.Roles

            .Include(x => x.RolePermissions)

            .ThenInclude(x => x.Permission)

            .FirstOrDefaultAsync(
                x => x.Id == roleId,
                cancellationToken);

    }
    public async Task<IEnumerable<Role>> GetRolesByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Role?> GetByCodeAsync(
        RoleCode code,
        CancellationToken cancellationToken = default)
    {

        return await _context.Roles
            .FirstOrDefaultAsync(
                x => x.Code == code,
                cancellationToken);

    }



    public override async Task<Role?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {

        return await _context.Roles
            .Include(x => x.RolePermissions)
            .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

    }

}