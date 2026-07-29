namespace BeautySalonBooking.WebApp.Settings
{
    public class AuthenticationEndpoints
    {
        public string RequestOtp { get; set; } = "api/Authentication/request-otp";
        public string VerifyOtp { get; set; } = "api/Authentication/verify-otp";
        public string RefreshToken { get; set; } = "api/Authentication/refresh-token";

    }
}
