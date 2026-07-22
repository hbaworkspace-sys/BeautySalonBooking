using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.ContactAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Enums;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

public class User : AuditableSoftDeleteEntity<long>
{
    public long PersonId { get; private set; }
    public Person Person { get; private set; } = null!;

    public UserRole UserRole { get; private set; }

    public string? UserName { get; private set; } = null!;
    public string? PasswordHash { get; private set; } = null!;
    public AuthenticationMode AuthenticationMode { get; private set; }
    public int? AccessFailedCount { get; private set; }
    public DateTime? LockoutEnd { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public DateTime? LastPasswordChangedAt { get; private set; }
    public bool? TwoFactorEnabled { get; private set; }
    public DateTime? LastFailedLoginAt { get; private set; }
    public DateTime? LastLogoutAt { get; private set; }
    public string? Tag1 { get; private set; }
    public string? Tag2 { get; private set; }
    public string? Tag3 { get; private set; }

    private readonly List<PhoneNumber> _phoneNumbers = [];
    public IReadOnlyCollection<PhoneNumber> PhoneNumbers => _phoneNumbers;

    private readonly List<OrganizationOwner> _organizationOwners = new();
    public IReadOnlyCollection<OrganizationOwner> OrganizationOwners => _organizationOwners;

    private User()
    {
    }
    public static User CreateCustomerUser(
    Person person,
    AuthenticationMode authenticationMode,
    string phoneNumber)
    {
        var user = new User
        {
            Person = person,
            AuthenticationMode = authenticationMode,
            LastPasswordChangedAt = DateTime.UtcNow
        };

        var mobile = PhoneNumber.Create(
            phoneNumber,
            PhoneNumberType.Mobile,
            true);

        user.AddPhoneNumber(mobile);

        return user;
    }
    public static User Create(
        long personId,
        string userName,
        string passwordHash,
        AuthenticationMode authenticationMode)
    {
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);

        return new User
        {
            PersonId = personId,
            UserName = userName.Trim(),
            PasswordHash = passwordHash,
            AuthenticationMode = authenticationMode,
            LastPasswordChangedAt = DateTime.UtcNow
        };
    }

    public void ChangeUserName(string userName)
    {
        ValidateUserName(userName);

        UserName = userName.Trim();
    }

    public void ChangePassword(string passwordHash)
    {
        ValidatePasswordHash(passwordHash);

        PasswordHash = passwordHash;
        LastPasswordChangedAt = DateTime.UtcNow;
    }

    public void ChangeAuthenticationMode(AuthenticationMode authenticationMode)
    {
        AuthenticationMode = authenticationMode;
    }
    public void AddPhoneNumber(PhoneNumber phoneNumber)
    {
        ArgumentNullException.ThrowIfNull(phoneNumber);

        if (_phoneNumbers.Any(x => x.Number == phoneNumber.Number))
            throw new InvalidOperationException("Phone number already exists.");

        if (phoneNumber.IsDefault)
        {
            foreach (var phone in _phoneNumbers)
                phone.RemoveAsDefault();
        }

        _phoneNumbers.Add(phoneNumber);
    }
    public void RemovePhoneNumber(string number)
    {
        var phone = _phoneNumbers.FirstOrDefault(x => x.Number == number);

        if (phone is null)
            return;

        _phoneNumbers.Remove(phone);
    }
    public void VerifyPhoneNumber(string number)
    {
        var phone = _phoneNumbers.FirstOrDefault(x => x.Number == number)
            ?? throw new InvalidOperationException("Phone number not found.");

        phone.Verify();
    }
    void SetDefaultPhoneNumber(string number)
    {
        var phone = _phoneNumbers.FirstOrDefault(x => x.Number == number)
            ?? throw new InvalidOperationException("Phone number not found.");

        foreach (var item in _phoneNumbers)
            item.RemoveAsDefault();

        phone.SetAsDefault();
    }

    public void RegisterSuccessfulLogin()
    {
        AccessFailedCount = 0;
        LastFailedLoginAt = null;
        LastLoginAt = DateTime.UtcNow;
    }

    public void RegisterFailedLogin()
    {
        AccessFailedCount++;
        LastFailedLoginAt = DateTime.UtcNow;
    }

    public void LockUntil(DateTime lockoutEnd)
    {
        LockoutEnd = lockoutEnd;
    }

    public void RegisterLogout()
    {
        LastLogoutAt = DateTime.UtcNow;
    }

    public void UpdateTags(string? tag1, string? tag2, string? tag3)
    {
        Tag1 = Normalize(tag1);
        Tag2 = Normalize(tag2);
        Tag3 = Normalize(tag3);
    }
    public void AddOrganizationOwner(OrganizationOwner organizationOwner)
    {
        ArgumentNullException.ThrowIfNull(organizationOwner);

        if (_organizationOwners.Any(x => x.Id == organizationOwner.Id))
            return;

        _organizationOwners.Add(organizationOwner);
    }

    public void RemoveOrganizationOwner(OrganizationOwner organizationOwner)
    {
        ArgumentNullException.ThrowIfNull(organizationOwner);

        var existing = _organizationOwners.FirstOrDefault(x => x.Id == organizationOwner.Id);

        if (existing is null)
            return;

        _organizationOwners.Remove(existing);
    }
    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Username cannot be empty.", nameof(userName));
    }

    private static void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));
    }

    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }

    public void LoginSucceeded()
    {
        AccessFailedCount = 0;
        LastFailedLoginAt = null;
        LastLoginAt = DateTime.UtcNow;
        LockoutEnd = null;
    }

    public void LoginFailed(int maxFailedAttempts, TimeSpan lockDuration)
    {
        if (maxFailedAttempts <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxFailedAttempts));

        if (lockDuration <= TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(lockDuration));

        AccessFailedCount++;
        LastFailedLoginAt = DateTime.UtcNow;

        if (AccessFailedCount >= maxFailedAttempts)
        {
            LockoutEnd = DateTime.UtcNow.Add(lockDuration);
        }
    }

    public bool IsLockedOut()
    {
        return LockoutEnd.HasValue && LockoutEnd > DateTime.UtcNow;
    }

    public void Unlock()
    {
        AccessFailedCount = 0;
        LockoutEnd = null;
        LastFailedLoginAt = null;
    }
}