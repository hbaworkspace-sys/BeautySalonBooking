using BeautySalonBooking.Maui.Features.Booking.ViewModels;

namespace BeautySalonBooking.Maui.Features.Booking.Views;

public partial class ConfirmationPage : ContentPage
{
    public ConfirmationPage(ConfirmationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}