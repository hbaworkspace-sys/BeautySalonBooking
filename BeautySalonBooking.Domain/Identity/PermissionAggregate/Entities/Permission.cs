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


    // =========================
    // Parent Permission
    // =========================

    public int? ParentId { get; private set; }

    public Permission? Parent { get; private set; }



    private readonly List<Permission> _children = new();

    public IReadOnlyCollection<Permission> Children => _children;



    // =========================
    // Role Permissions
    // =========================

    private readonly List<RolePermission> _rolePermissions = new();

    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;



    private Permission()
    {
    }



    // =========================
    // Factory Method
    // =========================

    public static Permission Create(
        string title,
        string code,
        PermissionType type,
        string? description = null,
        int? parentId = null)
    {

        ValidateTitle(title);

        ValidateCode(code);



        return new Permission
        {
            Title = title.Trim(),
            Code = code.Trim(),
            Type = type,
            Description = Normalize(description),
            ParentId = parentId
        };
    }



    // =========================
    // Update
    // =========================

    public void Update(
        string title,
        string code,
        PermissionType type,
        string? description,
        int? parentId)
    {

        ValidateTitle(title);

        ValidateCode(code);



        Title = title.Trim();

        Code = code.Trim();

        Type = type;

        Description = Normalize(description);

        ParentId = parentId;
    }



    // =========================
    // Change Methods
    // =========================

    public void ChangeTitle(string title)
    {
        ValidateTitle(title);

        Title = title.Trim();
    }



    public void ChangeCode(string code)
    {
        ValidateCode(code);

        Code = code.Trim();
    }



    public void ChangeDescription(string? description)
    {
        Description = Normalize(description);
    }



    public void ChangeType(PermissionType type)
    {
        Type = type;
    }



    public void ChangeParent(int? parentId)
    {
        ParentId = parentId;
    }



    // =========================
    // Validation
    // =========================

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Permission title is required.");
    }



    private static void ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "Permission code is required.");
    }



    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}