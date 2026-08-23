using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Main.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class ServiceSelectionViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public ServiceSelectionViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private Task GoToOrganizationSelectionAsync()
    {
        return _navigationService.GoToOrganizationSelectionAsync();
    }
    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}