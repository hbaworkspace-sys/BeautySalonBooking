using BeautySalonBooking.Maui.Common.Enums;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Profile.ViewModels;

public partial class ChangeMobileViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public ChangeMobileViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [ObservableProperty]    private string newMobileNumber = string.Empty;
    [ObservableProperty]    private bool isBusy;

    [RelayCommand]
    private async Task RequestMobileChangeAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            // فعلاً فقط برای تست Navigation
            await _navigationService.GoToOtpAsync(
                new OtpNavigationModel
                {
                    MobileNumber = NewMobileNumber,
                    Purpose = OtpPurpose.ChangeMobile
                });
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}