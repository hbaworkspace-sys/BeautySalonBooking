namespace BeautySalonBooking.Contracts.Booking.Availability.Dtos;

public sealed class AvailableDateDto
{
    public DateOnly Date { get; init; }

    public IReadOnlyList<AvailableTimeSlotDto> Slots { get; init; } = [];
}