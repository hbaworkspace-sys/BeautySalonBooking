namespace BeautySalonBooking.Contracts.Booking.Availability.Dtos;

public sealed class AvailableTimeSlotDto
{
    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }
}