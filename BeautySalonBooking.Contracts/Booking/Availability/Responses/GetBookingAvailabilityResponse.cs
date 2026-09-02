using BeautySalonBooking.Contracts.Booking.Availability.Dtos;

namespace BeautySalonBooking.Contracts.Booking.Availability.Responses;

public sealed class GetBookingAvailabilityResponse
{
    public IReadOnlyList<AvailableDateDto> Dates { get; init; } = [];
}