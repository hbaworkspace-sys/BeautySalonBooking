using BeautySalonBooking.Contracts.Booking.CreateBooking.Requests;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Components.Information.Models;
using BeautySalonBooking.Maui.Features.Booking.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class ConfirmationViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IBookingSelectionState _bookingSelectionState;
    private readonly IBookingResultState _bookingResultState;
    private readonly IBookingApiService _bookingApiService;

    public ConfirmationViewModel(
        INavigationService navigationService,
        IBookingSelectionState bookingSelectionState,
        IBookingResultState bookingResultState,
        IBookingApiService bookingApiService)
    {
        _navigationService = navigationService;
        _bookingSelectionState = bookingSelectionState;
        _bookingResultState = bookingResultState;
        _bookingApiService = bookingApiService;

        BuildSummary();
    }

    #region Summary

    [ObservableProperty]
    private List<InfoSummaryItem> summaryItems = [];

    private void BuildSummary()
    {
        var selection = _bookingSelectionState.Current;

        SummaryItems =
        [
            new InfoSummaryItem
            {
                Column1 = "content_cut",
                Column2 = selection.Service?.Title ?? "-"
            },

            new InfoSummaryItem
            {
                Column1 = "store",
                Column2 = selection.Branch?.OrganizationTitle ?? "-"
            },

            new InfoSummaryItem
            {
                Column1 = "location_on",
                Column2 = selection.Branch?.BranchDescription ?? "-"
            },

            new InfoSummaryItem
            {
                Column1 = "business",
                Column2 = selection.Branch?.BranchTitle ?? "-"
            },

            new InfoSummaryItem
            {
                Column1 = "person",
                Column2 = selection.Stylist?.FullName ?? "-"
            },

            new InfoSummaryItem
            {
                Column1 = "calendar_month",
                Column2 = selection.Date is null
                    ? "-"
                    : $"{selection.Date.DayName} {selection.Date.DayNumber} {selection.Date.MonthName}"
            },

            new InfoSummaryItem
            {
                Column1 = "schedule",
                Column2 = selection.Time?.DisplayTime ?? "-"
            },

            new InfoSummaryItem
            {
                Column1 = "hourglass_top",
                Column2 = selection.Service?.BaseDuration is null
                    ? "-"
                    : $"{selection.Service.BaseDuration} دقیقه"
            },

            new InfoSummaryItem
            {
                Column1 = "sell",
                Column2 = selection.Service?.BasePrice is null
                    ? "-"
                    : $"{selection.Service.BasePrice:N0} تومان",
                IsSeparatorVisible = false
            }
        ];
    }

    #endregion

    #region Booking

    [ObservableProperty]
    private bool isSubmitting;

    [RelayCommand]
    private async Task ConfirmBookingAsync()
    {
        if (IsSubmitting)
            return;

        var selection = _bookingSelectionState.Current;

        if (selection.Service is null ||
            selection.Branch is null ||
            selection.Stylist is null ||
            selection.Date is null ||
            selection.Time is null)
        {
            return;
        }

        var branchMemberServiceId =
            selection.Stylist.BranchMemberServiceId;

        if (branchMemberServiceId <= 0)
            return;

        IsSubmitting = true;

        try
        {
            var request = new CreateBookingRequest
            {
                // موقت تا زمان پیاده‌سازی Authentication
                CustomerUserId = 1,

                BranchMemberServiceId =
                    branchMemberServiceId,

                Date =
                    DateOnly.FromDateTime(
                        selection.Date.Date),

                StartTime =
                    TimeOnly.FromTimeSpan(
                        selection.Time.Time),

                Note = null
            };

            var result =
                await _bookingApiService
                    .CreateBookingAsync(request);

            if (!result.IsSuccess ||
                result.Payload is null)
            {
                return;
            }

            _bookingResultState.Set(result.Payload);

            _bookingSelectionState.Clear();

            await _navigationService
                .GoToBookingSuccessAsync();
        }
        finally
        {
            IsSubmitting = false;
        }
    }

    #endregion

    #region Navigation

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService
            .GoBackAsync();
    }

    #endregion
}