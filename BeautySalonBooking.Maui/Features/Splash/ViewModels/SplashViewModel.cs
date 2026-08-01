using BeautySalonBooking.Maui.Common.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalonBooking.Maui.Features.Splash;

public partial class SplashViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public SplashViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public async Task CheckLoginStatusAsync()
    {
        await _navigationService.GoToOnboardingAsync();
    }
}