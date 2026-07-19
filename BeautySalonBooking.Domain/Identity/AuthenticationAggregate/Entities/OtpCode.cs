using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;

public class OtpCode : AuditableEntity<long>
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string MobileNumber { get; private set; } = null!;
    public string CodeHash { get; private set; } = null!;
    public OtpPurpose Purpose { get; private set; }
    public DateTime? UsedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public int AttemptCount { get; private set; }
    public long? UserId { get; private set; }
    public User? User { get; private set; }
    public DateTime? LastAttemptAt { get; private set; }
}