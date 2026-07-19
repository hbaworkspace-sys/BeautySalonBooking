using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

public class Role : AuditableSoftDeleteEntity<int>
{
    public string Title { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public string? Description { get; private set; }


    private readonly List<RolePermission> _rolePermissions = new();
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;
}