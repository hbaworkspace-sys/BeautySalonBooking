using BeautySalonBooking.Maui.Features.Booking.ViewModels;

namespace BeautySalonBooking.Maui.Features.Booking.Views;

public partial class ServiceSelectionPage : ContentPage
{
    public ServiceSelectionPage(
        ServiceSelectionViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceSelectionViewModel viewModel)
        {
            await viewModel.LoadCategoriesAsync();
        }
    }
}