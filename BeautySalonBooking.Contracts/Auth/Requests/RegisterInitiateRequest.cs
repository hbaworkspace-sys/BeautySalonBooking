using BeautySalonBooking.Contracts.Common;

namespace BeautySalonBooking.Contracts.Auth.Requests
{
    public class RegisterInitiateRequest : PhoneNumberRequest
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
    }
}
