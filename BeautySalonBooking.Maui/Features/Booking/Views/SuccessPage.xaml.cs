using BeautySalonBooking.Maui.Features.Booking.ViewModels;

namespace BeautySalonBooking.Maui.Features.Booking.Views;

public partial class SuccessPage : ContentPage
{
    public SuccessPage(SuccessViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}