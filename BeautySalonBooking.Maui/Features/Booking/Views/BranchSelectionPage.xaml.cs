using BeautySalonBooking.Maui.Features.Booking.ViewModels;

namespace BeautySalonBooking.Maui.Features.Booking.Views;

public partial class BranchSelectionPage : ContentPage
{
    public BranchSelectionPage(BranchSelectionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is BranchSelectionViewModel viewModel)
        {
            await viewModel.LoadOrganizationsAsync();
        }
    }
}