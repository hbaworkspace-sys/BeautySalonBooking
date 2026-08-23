using BeautySalonBooking.Maui.Common.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class ConfirmationViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public ConfirmationViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    [RelayCommand]
    private Task GoToBookingSuccessAsync()
    {
        return _navigationService.GoToBookingSuccessAsync();
    }

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}