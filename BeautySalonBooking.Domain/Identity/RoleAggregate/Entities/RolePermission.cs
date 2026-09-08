using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

public class RolePermission : AuditableEntity<int>
{
    public int RoleId { get; private set; }

    public Role Role { get; private set; } = null!;

    public int PermissionId { get; private set; }

    public Permission Permission { get; private set; } = null!;

    private RolePermission()
    {
    }

    public static RolePermission Create(
        int roleId,
        int permissionId)
    {
        return new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        };
    }
}