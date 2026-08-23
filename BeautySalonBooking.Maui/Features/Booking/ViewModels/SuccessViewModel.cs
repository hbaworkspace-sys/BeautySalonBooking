using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Main.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class SuccessViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public SuccessViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private Task GoToDashboardAsync()
    {
        return _navigationService.GoToMainAsync(MainTab.Dashboard);
    }
    [RelayCommand]
    private Task GoToAppointmentsAsync()
    {
        return _navigationService.GoToMainAsync(MainTab.Appointments);
    }
}
