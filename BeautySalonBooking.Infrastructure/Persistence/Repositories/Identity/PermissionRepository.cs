using BeautySalonBooking.Domain.Repositories;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;


public class PermissionRepository
    : Repository<Permission, int>,
      IPermissionRepository
{


    private readonly BeautyDbContext _context;



    public PermissionRepository(
        BeautyDbContext context)
        : base(context)
    {
        _context = context;
    }


    public async Task<List<Permission>> GetRootAsync(
CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .Where(x => x.ParentId == null)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }



    public async Task<List<Permission>> GetChildrenAsync(
    int parentId,
    CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .Where(x => x.ParentId == parentId)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }
    public async Task<List<Permission>> GetRootPermissionsAsync(
        CancellationToken cancellationToken = default)
    {

        return await _context.Permissions
            .Where(x => x.ParentId == null)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

    }


    public async Task DeletePermissiByIdAsync(int id, long userId, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Permissions.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (entity != null)
        {
            entity.Delete(userId);
        }
    }

    public async Task<Permission?> GetByIdWithChildrenAsync(
        int id,
        CancellationToken cancellationToken = default)
    {


        return await _context.Permissions
            .Include(x => x.Children)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);


    }




    public async Task<bool> HasChildrenAsync(
        int id,
        CancellationToken cancellationToken = default)
    {

        return await _context.Permissions
            .AnyAsync(
                x => x.ParentId == id,
                cancellationToken);

    }


    public async Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {

        var role =
            await _context.UserRoles
            .FirstOrDefaultAsync(
                x => x.UserId == userId,
                cancellationToken);


        if (role == null)
            return new List<Permission>();


        return await _context.RolePermissions
            .Where(x => x.RoleId == role.RoleId)
            .Select(x => x.Permission)
            .ToListAsync(cancellationToken);
    }
}