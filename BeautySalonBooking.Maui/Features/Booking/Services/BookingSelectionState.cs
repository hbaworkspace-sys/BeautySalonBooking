using BeautySalonBooking.Maui.Features.Booking.Models;

namespace BeautySalonBooking.Maui.Features.Booking.Services;

public sealed class BookingSelectionState : IBookingSelectionState
{
    public BookingSelection Current { get; } = new();

    public void Clear()
    {
        Current.Service = null;
        Current.Branch = null;
        Current.Stylist = null;
        Current.Date = null;
        Current.Time = null;
    }
}