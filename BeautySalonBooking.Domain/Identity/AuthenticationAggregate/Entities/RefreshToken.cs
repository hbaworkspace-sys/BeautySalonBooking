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

    public string Token { get; private set; } = string.Empty;
    public long UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }

    // Navigation Property
    public User User { get; private set; } = null!;

    private RefreshToken()
    {
    }

    public static RefreshToken Create(
        string token,
        long userId,
        DateTime expiresAt)
    {


        return new RefreshToken
        {
            IsRevoked = false,
            UserId = userId,
            ExpiresAt = expiresAt,
            Token = token,
        };
    }

    public void SetRevokedToActive()
    {
        if (IsRevoked) { return; }

        IsRevoked = true;
    }
    public void SetRevokedToDeActive()
    {
        if (IsRevoked == false) return;

        IsRevoked = false;
    }
}
