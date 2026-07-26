using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Contracts.Authentication.Requests;

public class VerifyOtpRequest : MobileNumberRequest
{
    public string OtpCode { get; init; } = string.Empty;
}