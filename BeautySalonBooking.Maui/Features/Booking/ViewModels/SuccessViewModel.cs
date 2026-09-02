using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Components.Information.Models;
using BeautySalonBooking.Maui.Features.Booking.Services;
using BeautySalonBooking.Maui.Features.Main.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class SuccessViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IBookingResultState _bookingResultState;

    public SuccessViewModel(
        INavigationService navigationService,
        IBookingResultState bookingResultState)
    {
        _navigationService = navigationService;
        _bookingResultState = bookingResultState;

        BuildSummary();
    }

    #region Summary

    [ObservableProperty]
    private List<InfoSummaryItem> summaryItems = [];

    [ObservableProperty]
    private long appointmentId;

    private void BuildSummary()
    {
        var result = _bookingResultState.Result;

        if (result is null)
            return;

        AppointmentId = result.AppointmentId;

        SummaryItems =
        [
            new InfoSummaryItem
            {
                Column2 = result.ServiceTitle,
                Column3 = "سرویس",
                IsSeparatorVisible = false
            },

            new InfoSummaryItem
            {
                Column2 = result.StylistName,
                Column3 = "استایلیست",
                IsSeparatorVisible = false
            },

            new InfoSummaryItem
            {
                Column2 = string.IsNullOrWhiteSpace(result.BranchDescription)
                    ? $"{result.OrganizationTitle} - {result.BranchTitle}"
                    : $"{result.OrganizationTitle} - {result.BranchTitle} - {result.BranchDescription}",
                Column3 = "سالن",
                IsSeparatorVisible = false
            },

            new InfoSummaryItem
            {
                Column2 =
                    $"{result.Date:yyyy/MM/dd}",
                Column3 = "تاریخ",
                IsSeparatorVisible = false
            },

            new InfoSummaryItem
            {
                Column2 =
                    $"{result.StartTime:HH\\:mm} - {result.EndTime:HH\\:mm}",
                Column3 = "ساعت",
                IsSeparatorVisible = false
            },

            new InfoSummaryItem
            {
                Column2 =
                    $"{result.Duration.TotalMinutes:0} دقیقه",
                Column3 = "مدت زمان",
                IsSeparatorVisible = false
            },

            new InfoSummaryItem
            {
                Column2 =
                    $"{result.TotalPrice:N0} تومان",
                Column3 = "قیمت",
                IsSeparatorVisible = false
            }
        ];
    }

    #endregion

    #region Navigation

    [RelayCommand]
    private Task GoToDashboardAsync()
    {
        return _navigationService
            .GoToMainAsync(MainTab.Dashboard);
    }

    [RelayCommand]
    private Task GoToAppointmentsAsync()
    {
        return _navigationService
            .GoToMainAsync(MainTab.Appointments);
    }
    #endregion
}
