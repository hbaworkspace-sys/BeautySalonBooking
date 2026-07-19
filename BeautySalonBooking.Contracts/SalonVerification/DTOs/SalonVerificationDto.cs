namespace BeautySalonBooking.Contracts.SalonVerifications
{
    public class SalonVerificationDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string LicenceNumber { get; init; } = string.Empty;
        public int Status { get; init; }
        public DateTime SubmittedAt { get; init; }
    }
}