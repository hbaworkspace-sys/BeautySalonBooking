using BeautySalonBooking.Maui.Features.Booking.ViewModels;

namespace BeautySalonBooking.Maui.Features.Booking.Views;

public partial class OrganizationSelectionPage : ContentPage
{
    public OrganizationSelectionPage(OrganizationSelectionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}