namespace BeautySalonBooking.Contracts.SalonVerifications
{
    public class RegisterSalonVerificationRequest
    {
        public Guid OwnerUserId { get; set; }
        public string Name { get; init; } = string.Empty;

        public string LicenceNumber { get; init; } = string.Empty;

        public string LandlinePhoneNumber { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;

        public int RegionId { get; init; }

        public string PostalCode { get; init; } = string.Empty;

        public string Address { get; init; } = string.Empty;
    }
}