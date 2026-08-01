using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth;

namespace BeautySalonBooking.Maui.Features.Splash.Views;

public partial class OnboardingPage : ContentPage
{
    private readonly INavigationService _navigationService;
    public OnboardingPage(INavigationService navigationService)
    {
        InitializeComponent();

        _navigationService = navigationService;
    }
    private async void OnboardingCardView_OnboardingCompleted(object sender, EventArgs e)
    {
        await _navigationService.GoToLoginAsync();
    }
}