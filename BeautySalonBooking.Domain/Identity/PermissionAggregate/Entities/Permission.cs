using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.PermissionAggregate.Enums;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.PermissionAggregate.Entities;

public class Permission : AuditableSoftDeleteEntity<int>
{
    public string Title { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public PermissionType Type { get; private set; }
    public string? Description { get; private set; }


    private readonly List<RolePermission> _rolePermissions = new();
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;
}