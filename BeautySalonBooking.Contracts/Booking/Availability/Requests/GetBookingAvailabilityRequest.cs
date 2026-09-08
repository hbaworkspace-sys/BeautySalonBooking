namespace BeautySalonBooking.Contracts.Booking.Availability.Requests;

public sealed class GetBookingAvailabilityRequest
{
    public long BranchMemberServiceId { get; init; }

    public DateOnly FromDate { get; init; }

    public DateOnly ToDate { get; init; }

    public TimeSpan SlotInterval { get; init; }
}