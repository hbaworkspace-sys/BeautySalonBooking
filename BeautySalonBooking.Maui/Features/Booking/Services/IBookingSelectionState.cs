using BeautySalonBooking.Maui.Features.Booking.Models;

namespace BeautySalonBooking.Maui.Features.Booking.Services;

public interface IBookingSelectionState
{
    BookingSelection Current { get; }

    void Clear();
}