using BeautySalonBooking.Maui.Features.Booking.ViewModels;

namespace BeautySalonBooking.Maui.Features.Booking.Views;

public partial class StylistSelectionPage : ContentPage
{
    public StylistSelectionPage(
        StylistSelectionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is StylistSelectionViewModel viewModel)
        {
            await viewModel.LoadMembersAsync();
        }
    }
}