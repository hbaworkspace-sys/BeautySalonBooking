namespace BeautySalonBooking.Contracts.Auth
{
    public class UserDto
    {
        public long Id { get; init; }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public string? FullName => $"{FirstName} {LastName}";
    }
}