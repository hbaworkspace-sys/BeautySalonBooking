using BeautySalonBooking.Maui.Common.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Profile.ViewModels;

public partial class NotificationSettingsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public NotificationSettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}