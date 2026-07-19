using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Enums;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

public class User : AuditableSoftDeleteEntity<long>
{
    public long PersonId { get; private set; }
    public Person Person { get; private set; } = null!;

    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public AuthenticationMode AuthenticationMode { get; private set; }
    public int AccessFailedCount { get; private set; }
    public DateTime? LockoutEnd { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public DateTime? LastPasswordChangedAt { get; private set; }
    public bool TwoFactorEnabled { get; private set; }
    public DateTime? LastFailedLoginAt { get; private set; }
    public DateTime? LastLogoutAt { get; private set; }
    public string? Tag1 { get; private set; }
    public string? Tag2 { get; private set; }
    public string? Tag3 { get; private set; }


    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles;


    private readonly List<OrganizationOwner> _organizationOwners = new();
    public IReadOnlyCollection<OrganizationOwner> OrganizationOwners => _organizationOwners;
}