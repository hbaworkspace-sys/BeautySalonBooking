using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Main.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class OrganizationSelectionViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public OrganizationSelectionViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private Task GoToStylistSelectionAsync()
    {
        return _navigationService.GoToStylistSelectionAsync();
    }
    
    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}