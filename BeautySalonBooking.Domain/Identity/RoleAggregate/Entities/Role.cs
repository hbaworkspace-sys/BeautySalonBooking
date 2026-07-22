using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Enums;

namespace BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

public class Role : AuditableSoftDeleteEntity<int>
{
    public string Title { get; private set; } = default!;
    public RoleCode Code { get; private set; }
    public string? Description { get; private set; }


    private readonly List<RolePermission> _rolePermissions = new();
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;

    private Role()
    {
    }
    public static Role Create(
        string title,
        RoleCode roleCode,
        string? description = null)
    {
        Validate(title);

        return new Role
        {
            Title = title.Trim(),
            Code = roleCode,
            Description = Normalize(description)
        };
    }
    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Role title cannot be empty.", nameof(title));

        Title = title.Trim();
    }

    public void ChangeDescription(string? description)
    {
        Description = Normalize(description);
    }

    public void AddPermission(RolePermission permission)
    {
        ArgumentNullException.ThrowIfNull(permission);

        if (_rolePermissions.Any(x => x.PermissionId == permission.PermissionId))
            return;

        _rolePermissions.Add(permission);
    }

    public void RemovePermission(RolePermission permission)
    {
        ArgumentNullException.ThrowIfNull(permission);

        var existing = _rolePermissions
            .FirstOrDefault(x => x.PermissionId == permission.PermissionId);

        if (existing is null)
            return;

        _rolePermissions.Remove(existing);
    }
    private static void Validate(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Role title cannot be empty.", nameof(title));
    }

    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}