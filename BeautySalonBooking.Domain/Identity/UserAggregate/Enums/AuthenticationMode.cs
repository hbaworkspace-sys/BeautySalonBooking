namespace BeautySalonBooking.Domain.Identity.UserAggregate.Enums;

public enum AuthenticationMode : byte
{
    Password = 1,
    Sms = 2,
    Email = 3,
    Google = 4,
    Microsoft = 5
}