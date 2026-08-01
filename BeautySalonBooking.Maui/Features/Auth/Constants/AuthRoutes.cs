namespace BeautySalonBooking.Maui.Features.Auth.Constants;

public static class AuthRoutes
{
    public const string RegisterInitiate = "api/Authentication/register/request-otp";
    public const string RegisterVerifyOtp = "api/Authentication/register/verify-otp";
    public const string LoginInitiate = "api/Authentication/login/request-otp";
    public const string LoginVerifyOtp = "api/Authentication/login/verify-otp";
}