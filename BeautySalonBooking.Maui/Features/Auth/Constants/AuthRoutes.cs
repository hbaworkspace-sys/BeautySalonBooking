namespace BeautySalonBooking.Maui.Features.Auth.Constants;

public static class AuthRoutes
{
    public const string RegisterInitiate = "api/authentication/register/request-otp";
    public const string RegisterVerifyOtp = "api/authentication/register/verify-otp";
    public const string LoginInitiate = "api/authentication/login/request-otp";
    public const string LoginVerifyOtp = "api/authentication/login/verify-otp";

    public const string RefreshToken = "api/authentication/refresh-token";
    public const string Me = "api/Authentication/me";
}