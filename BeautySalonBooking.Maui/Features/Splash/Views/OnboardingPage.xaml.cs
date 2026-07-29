using BeautySalonBooking.Maui.Features.Auth;

namespace BeautySalonBooking.Maui.Features.Splash.Views;

public partial class OnboardingPage : ContentPage
{
    public OnboardingPage()
    {
        InitializeComponent();
    }
    private async void OnboardingCardView_OnboardingCompleted(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}