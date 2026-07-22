using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;

public class OtpCode : AuditableEntity<long>
{
    public string MobileNumber { get; private set; } = null!;
    public string CodeHash { get; private set; } = null!;
    public OtpPurpose Purpose { get; private set; }
    public DateTime ExpiresAt { get; private set; }

    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public long? UserId { get; private set; }
    public User? User { get; private set; }

    public DateTime? UsedAt { get; private set; }
    public int AttemptCount { get; private set; }
    public DateTime? LastAttemptAt { get; private set; }

    private OtpCode()
    {
    }

    public static OtpCode Create(
        string mobileNumber,
        string codeHash,
        OtpPurpose purpose,
        DateTime expiresAt,
        string? firstName = null,
        string? lastName = null,
        long? userId = null)
    {
        ValidateMobileNumber(mobileNumber);
        ValidateCodeHash(codeHash);

        return new OtpCode
        {
            MobileNumber = mobileNumber.Trim(),
            CodeHash = codeHash,
            Purpose = purpose,
            ExpiresAt = expiresAt,
            FirstName = Normalize(firstName),
            LastName = Normalize(lastName),
            UserId = userId,
        };
    }

    public void RegisterAttempt()
    {
        AttemptCount++;
        LastAttemptAt = DateTime.UtcNow;
    }

    public void MarkAsUsed()
    {
        if (UsedAt.HasValue)
            return;

        UsedAt = DateTime.UtcNow;
    }

    public void ChangeExpiration(DateTime expiresAt)
    {
        ExpiresAt = expiresAt;
    }

    public void ChangeCode(string codeHash, DateTime expiresAt)
    {
        ValidateCodeHash(codeHash);

        CodeHash = codeHash;
        ExpiresAt = expiresAt;

        UsedAt = null;
        AttemptCount = 0;
        LastAttemptAt = null;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow >= ExpiresAt;
    }

    public bool IsUsed()
    {
        return UsedAt.HasValue;
    }

    public bool CanBeUsed()
    {
        return !IsUsed() || !IsExpired();
    }

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new ArgumentException("Mobile number cannot be empty.", nameof(mobileNumber));
    }

    private static void ValidateCodeHash(string codeHash)
    {
        if (string.IsNullOrWhiteSpace(codeHash))
            throw new ArgumentException("Code hash cannot be empty.", nameof(codeHash));
    }

    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}