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

        await Task.Delay(6000);

        await ((SplashViewModel)BindingContext).CheckLoginStatusAsync();
    }
}