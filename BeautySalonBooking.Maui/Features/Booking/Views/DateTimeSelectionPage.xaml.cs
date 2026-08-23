using BeautySalonBooking.Maui.Features.Booking.ViewModels;

namespace BeautySalonBooking.Maui.Features.Booking.Views;

public partial class DateTimeSelectionPage : ContentPage
{
    public DateTimeSelectionPage(DateTimeSelectionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}