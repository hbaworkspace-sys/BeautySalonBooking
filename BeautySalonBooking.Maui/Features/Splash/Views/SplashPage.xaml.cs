using BeautySalonBooking.Maui.Features.Splash.ViewModels;

namespace BeautySalonBooking.Maui.Features.Splash.Views;

public partial class SplashPage : ContentPage
{
    public SplashPage(SplashViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SplashViewModel vm)
        {
            await vm.InitializeAsync(
                Indicator.StartAnimationAsync());
        }
    }
}