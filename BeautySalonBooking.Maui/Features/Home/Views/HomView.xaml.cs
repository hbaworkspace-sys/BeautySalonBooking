using BeautySalonBooking.Maui.Features.Home.ViewModels;

namespace BeautySalonBooking.Maui.Features.Home.Views;

public partial class HomeView : ContentView
{
    private readonly HomeViewModel _viewModel;

    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        await _viewModel.LoadCategoriesAsync();
    }
}