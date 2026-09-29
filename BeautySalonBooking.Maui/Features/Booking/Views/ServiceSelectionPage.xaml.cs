using System.ComponentModel;
using BeautySalonBooking.Maui.Features.Booking.ViewModels;

namespace BeautySalonBooking.Maui.Features.Booking.Views;

public partial class ServiceSelectionPage : ContentPage
{
    private readonly ServiceSelectionViewModel _viewModel;

    public ServiceSelectionPage(
        ServiceSelectionViewModel vm)
    {
        InitializeComponent();

        _viewModel = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;

        await _viewModel.LoadCategoriesAsync();
    }

    protected override void OnDisappearing()
    {
        _viewModel.PropertyChanged -= OnViewModelPropertyChanged;

        base.OnDisappearing();
    }

    private async void OnViewModelPropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ServiceSelectionViewModel.IsSubCategoriesVisible) &&
            _viewModel.IsSubCategoriesVisible)
        {
            await ScrollDownAsync();
        }

        if (e.PropertyName == nameof(ServiceSelectionViewModel.IsServicesVisible) &&
            _viewModel.IsServicesVisible)
        {
            await ScrollDownAsync();
        }
    }

    private async Task ScrollDownAsync()
    {
        await Task.Delay(100);

        var targetY = MainScrollView.ScrollY + 300;

        await MainScrollView.ScrollToAsync(
            0,
            targetY,
            true);
    }
}