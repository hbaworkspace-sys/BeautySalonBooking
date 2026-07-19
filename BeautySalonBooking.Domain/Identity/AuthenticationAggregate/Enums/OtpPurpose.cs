namespace BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Enums;

public enum OtpPurpose : byte
{
    Register = 1,
    Login = 2,
    ForgotPassword = 3,
    ChangeMobileNumber = 4,
    VerifyMobileNumber = 5,
    TwoFactorAuthentication = 6
}