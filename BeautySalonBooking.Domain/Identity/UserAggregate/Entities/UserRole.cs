using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.RoleAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

public class UserRole : AuditableSoftDeleteEntity<int>
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public int RoleId { get; private set; }
    public Role Role { get; private set; } = null!;
    public bool IsPrimary { get; private set; }
}