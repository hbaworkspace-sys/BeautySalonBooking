using BeautySalonBooking.Contracts.Booking.CreateBooking.Responses;

namespace BeautySalonBooking.Maui.Features.Booking.Services;

public interface IBookingResultState
{
    CreateBookingResponse? Result { get; }

    void Set(CreateBookingResponse result);

    void Clear();
}