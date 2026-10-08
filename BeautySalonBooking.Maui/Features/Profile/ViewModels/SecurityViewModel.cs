using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Profile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BeautySalonBooking.Maui.Features.Profile.ViewModels;

public partial class SecurityViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public SecurityViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        ActiveDevices =
        [
            new ActiveDeviceModel
            {
                DeviceName = "Samsung Galaxy S24",
                LastActivity = "همین دستگاه",
                IsCurrentDevice = true
            },
            new ActiveDeviceModel
            {
                DeviceName = "iPhone 15 Pro",
                LastActivity = "۱۰ مهر ۱۴۰۳",
                IsCurrentDevice = false
            }
        ];
    }

    public ObservableCollection<ActiveDeviceModel> ActiveDevices { get; }

    [ObservableProperty] private bool isBiometricEnabled;

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }

    [RelayCommand]
    private Task GoToChangeMobileAsync()
    {
        return _navigationService.GoToChangeMobileAsync();
    }

    [RelayCommand]
    private void LogoutDevice(ActiveDeviceModel? device)
    {
        if (device is null || device.IsCurrentDevice)
            return;

        ActiveDevices.Remove(device);
    }

    [RelayCommand]
    private void LogoutAllDevices()
    {
        var otherDevices = ActiveDevices
            .Where(x => !x.IsCurrentDevice)
            .ToList();

        foreach (var device in otherDevices)
            ActiveDevices.Remove(device);
    }
}