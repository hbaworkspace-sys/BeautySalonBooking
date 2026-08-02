using BeautySalonBooking.Maui.Common.Interfaces;
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
    public Task GoToDashbordPage()
    {
        return _navigationService.GoToDashboardAsync();
    }
}
