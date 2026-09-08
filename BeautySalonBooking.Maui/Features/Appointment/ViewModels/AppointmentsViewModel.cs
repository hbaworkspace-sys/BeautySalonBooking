using System.Collections.ObjectModel;
using BeautySalonBooking.Contracts.Appointment.Dtos;
using BeautySalonBooking.Contracts.Appointment.Enums;
using BeautySalonBooking.Contracts.Appointment.Requests;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Appointment.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace BeautySalonBooking.Maui.Features.Appointment.ViewModels;

public partial class AppointmentsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IAppointmentApiService _appointmentApiService;

    private AppointmentListType _currentListType =
        AppointmentListType.Upcoming;

    public AppointmentsViewModel(
        INavigationService navigationService,
        IAppointmentApiService appointmentApiService)
    {
        _navigationService = navigationService;
        _appointmentApiService = appointmentApiService;
    }

    public ObservableCollection<AppointmentDto> VisibleAppointments { get; } = [];

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string? errorMessage;

    public async Task InitializeAsync()
    {
        await LoadAppointmentsAsync(_currentListType);
    }

    [RelayCommand]
    private async Task ChangeAppointmentListAsync(
        AppointmentListType listType)
    {
        _currentListType = listType;

        await LoadAppointmentsAsync(listType);
    }

    public async Task LoadAppointmentsAsync(
        AppointmentListType listType)
    {
        if (IsLoading)
            return;

        IsLoading = true;
        ErrorMessage = null;

        try
        {
            var request = new GetAppointmentsRequest
            {
                CustomerUserId = 1,
                ListType = listType
            };

            var result =
                await _appointmentApiService
                    .GetAppointmentsAsync(request);

            if (!result.IsSuccess || result.Payload is null)
            {
                VisibleAppointments.Clear();

                ErrorMessage =
                    string.IsNullOrWhiteSpace(result.Message)
                        ? "دریافت رزروها با خطا مواجه شد."
                        : result.Message;

                return;
            }

            VisibleAppointments.Clear();

            foreach (var appointment in result.Payload.Appointments)
            {
                VisibleAppointments.Add(appointment);
            }
        }
        catch (Exception ex)
        {
            VisibleAppointments.Clear();

            ErrorMessage =
                "دریافت رزروها با خطا مواجه شد.";

            System.Diagnostics.Debug.WriteLine(ex);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private Task GoToServiceSelectionAsync()
    {
        return _navigationService.GoToServiceSelectionAsync();
    }
}