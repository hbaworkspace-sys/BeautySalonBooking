using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Contracts.Auth.Requests
{
    public class VerifyOtpRequest : MobileNumberRequest
    {
        public string OtpCode { get; init; } = string.Empty;
    }
}