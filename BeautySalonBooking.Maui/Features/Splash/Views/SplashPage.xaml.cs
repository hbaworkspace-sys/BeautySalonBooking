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

        await Indicator.StartAnimationAsync();

        if (BindingContext is SplashViewModel vm)
        {
            await vm.CheckLoginStatusAsync();
        }
    }
}