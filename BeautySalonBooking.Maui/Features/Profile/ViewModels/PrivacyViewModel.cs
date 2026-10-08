using BeautySalonBooking.Maui.Common.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Profile.ViewModels;

public partial class PrivacyViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public PrivacyViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}
