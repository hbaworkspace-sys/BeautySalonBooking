using BeautySalonBooking.Maui.Common.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class StylistSelectionViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public StylistSelectionViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private Task GoToBookingDateTimeSelectionAsync()
    {
        return _navigationService.GoToBookingDateTimeSelectionAsync();
    }

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}