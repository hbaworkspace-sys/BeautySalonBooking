using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Eunms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;

public class RefreshToken : AuditableEntity<long>
{
    public string TokenHash { get; private set; } = string.Empty;

    public long UserId { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public bool IsRevoked { get; private set; }

    public DateTime? RevokedAt { get; private set; }

    public string? ReplacedByTokenHash { get; private set; }


    public Guid DeviceId { get; private set; }

    public string DeviceName { get; private set; } = string.Empty;

    public string IpAddress { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? RevokedAtUtc { get; private set; }

    public User User { get; private set; } = null!;

    private RefreshToken()
    {
    }

    public static RefreshToken Create(

    string tokenHash,

    long userId,

    DateTime expiresAt,

    Guid deviceId,

    string deviceName,

    string ipAddress)

    {
        return new RefreshToken
        {
            TokenHash = tokenHash,

            UserId = userId,

            ExpiresAt = expiresAt,

            DeviceId = deviceId,

            DeviceName = deviceName,

            IpAddress = ipAddress,

            CreatedAtUtc = DateTime.UtcNow,

            IsRevoked = false
        };
    }

    //public void Revoke()
    //{
    //    if (IsRevoked)
    //        return;

    //    IsRevoked = true;

    //    RevokedAtUtc = DateTime.UtcNow;
    //}
    public void Revoke(string? replacedByTokenHash = null)
    {
        if (IsRevoked)
            return;

        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
        ReplacedByTokenHash = replacedByTokenHash;
    }
}