using BeautySalonBooking.Contracts.Authentication.Enums;
using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Contracts.Authentication.Requests;

public class RegisterInitiateRequest : MobileNumberRequest
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string NationalCode { get; init; } = string.Empty;
    public Gender Gender { get; init; }
}
