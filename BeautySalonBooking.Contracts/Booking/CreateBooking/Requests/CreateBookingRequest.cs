namespace BeautySalonBooking.Contracts.Booking.CreateBooking.Requests;

public sealed class CreateBookingRequest
{
    public long CustomerUserId { get; init; } = 1;

    public long BranchMemberServiceId { get; init; }

    public DateOnly Date { get; init; }

    public TimeOnly StartTime { get; init; }

    public string? Note { get; init; }
}