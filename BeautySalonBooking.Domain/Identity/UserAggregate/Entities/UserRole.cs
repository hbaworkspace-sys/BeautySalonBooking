using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

public class UserRole : AuditableSoftDeleteEntity<long>
{
    public long UserId { get; private set; }
    public User User { get; private set; } = default!;
    public int RoleId { get; private set; }
    public Role Role { get; private set; } = default!;

    private UserRole()
    {
    }

    public static UserRole Create(
        User user,
        Role role)
    {
        return new UserRole
        {
            User = user,
            Role = role
        };
    }
    public void ChangeRole(int roleId)
    {
        RoleId = roleId;
    }
}