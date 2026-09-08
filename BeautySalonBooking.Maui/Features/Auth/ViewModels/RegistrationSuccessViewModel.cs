using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Main.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Auth.ViewModels;

public partial class RegistrationSuccessViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public RegistrationSuccessViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public Task GoToMainPage()
    {
        return _navigationService.GoToMainAsync(MainTab.Dashboard);
    }
}
