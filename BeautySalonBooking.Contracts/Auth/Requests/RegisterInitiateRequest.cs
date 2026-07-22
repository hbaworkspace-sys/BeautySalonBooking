using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Contracts.Auth.Requests
{
    public class RegisterInitiateRequest : MobileNumberRequest
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
    }
}
