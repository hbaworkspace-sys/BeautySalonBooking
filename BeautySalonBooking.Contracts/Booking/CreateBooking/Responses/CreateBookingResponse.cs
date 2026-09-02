namespace BeautySalonBooking.Contracts.Booking.CreateBooking.Responses;

public sealed class CreateBookingResponse
{
    public long AppointmentId { get; init; }

    public string ServiceTitle { get; init; } = string.Empty;

    public string OrganizationTitle { get; init; } = string.Empty;

    public string BranchTitle { get; init; } = string.Empty;

    public string BranchDescription { get; init; } = string.Empty;

    public string StylistName { get; init; } = string.Empty;

    public DateOnly Date { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }

    public TimeSpan Duration { get; init; }

    public decimal TotalPrice { get; init; }
}