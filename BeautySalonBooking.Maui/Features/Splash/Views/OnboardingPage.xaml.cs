using BeautySalonBooking.Maui.Common.Interfaces;

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
    protected override void OnAppearing()
    {
        base.OnAppearing();

        OnboardingCardView.StartRotation();
    }

    protected override void OnDisappearing()
    {
        OnboardingCardView.StopRotation();

        base.OnDisappearing();
    }
}