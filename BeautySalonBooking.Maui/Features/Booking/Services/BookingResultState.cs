using BeautySalonBooking.Contracts.Booking.CreateBooking.Responses;

namespace BeautySalonBooking.Maui.Features.Booking.Services;

public sealed class BookingResultState : IBookingResultState
{
    public CreateBookingResponse? Result { get; private set; }

    public void Set(CreateBookingResponse result)
    {
        Result = result;
    }

    public void Clear()
    {
        Result = null;
    }
}