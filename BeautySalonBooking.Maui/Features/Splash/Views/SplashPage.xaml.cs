using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Splash.Views;

namespace BeautySalonBooking.Maui.Features.Splash;

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
        Indicator_Border1.Background = Indicator_Border2.Background = Indicator_Border3.Background = Colors.LightGray;

        await Task.Delay(1200);
        Indicator_Border1.Background = Colors.PaleVioletRed;
        await Task.Delay(1200);
        Indicator_Border2.Background = Colors.PaleVioletRed;
        await Task.Delay(1200);
        Indicator_Border3.Background = Colors.PaleVioletRed;
        await Task.Delay(3000);

        await Shell.Current.GoToAsync(nameof(LoginPage));

        await ((SplashViewModel)BindingContext).CheckLoginStatusAsync();
    }
}