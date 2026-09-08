using BeautySalonBooking.Contracts.Booking.Availability.Requests;
using BeautySalonBooking.Contracts.Booking.Availability.Responses;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;
using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Components.DateAndTime.Models;
using BeautySalonBooking.Maui.Features.Booking.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class DateTimeSelectionViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IBookingApiService _bookingAvailabilityApiService;
    private readonly IBookingSelectionState _bookingSelectionState;

    // تمام Availability دریافت‌شده از سرور
    private GetBookingAvailabilityResponse? _availability;

    public DateTimeSelectionViewModel(
        INavigationService navigationService,
        IBookingApiService bookingAvailabilityApiService,
      IBookingSelectionState bookingSelectionState)
    {
        _navigationService = navigationService;
        _bookingAvailabilityApiService = bookingAvailabilityApiService;
        _bookingSelectionState = bookingSelectionState;
    }

    #region Selected Member

    public BranchMemberServiceDto? SelectedMember =>
        _bookingSelectionState.Current.Stylist;

    #endregion

    #region Dates

    [ObservableProperty]
    private ObservableCollection<DateSelectionItem> availableDates = new();

    [ObservableProperty]
    private DateSelectionItem? selectedDate;

    [ObservableProperty]
    private string selectedDateTitle = string.Empty;

    [ObservableProperty]
    private bool isDatesLoading;

    partial void OnSelectedDateChanged(
        DateSelectionItem? value)
    {
        if (value is null)
        {
            SelectedDateTitle = string.Empty;

            AvailableTimes.Clear();
            SelectedTime = null;

            _bookingSelectionState.Current.Date = null;
            _bookingSelectionState.Current.Time = null;

            return;
        }

        SelectedDateTitle =
            $"تایم‌های خالی - {value.DayName} {value.DayNumber} {value.MonthName}";

        // ذخیره تاریخ انتخاب‌شده
        _bookingSelectionState.Current.Date = value;

        // با تغییر تاریخ، زمان قبلی دیگر معتبر نیست.
        _bookingSelectionState.Current.Time = null;

        LoadAvailableTimes(value);
    }

    #endregion

    #region Times

    [ObservableProperty]
    private ObservableCollection<TimeSelectionItem> availableTimes = new();

    [ObservableProperty]
    private TimeSelectionItem? selectedTime;

    partial void OnSelectedTimeChanged(
        TimeSelectionItem? value)
    {
        if (value is null)
        {
            _bookingSelectionState.Current.Time = null;
            return;
        }

        // ذخیره زمان انتخاب‌شده
        _bookingSelectionState.Current.Time = value;
    }

    private void LoadAvailableTimes(
        DateSelectionItem date)
    {
        if (_availability?.Dates is null)
            return;

        var availableDate =
            _availability.Dates.FirstOrDefault(x =>
                x.Date == DateOnly.FromDateTime(date.Date));

        if (availableDate?.Slots is null ||
            availableDate.Slots.Count == 0)
        {
            AvailableTimes = new ObservableCollection<TimeSelectionItem>();
            SelectedTime = null;
            return;
        }

        var times = availableDate.Slots
            .Select(slot => new TimeSelectionItem
            {
                Time = slot.StartTime.ToTimeSpan(),
                DisplayTime =
                    $"{slot.StartTime:hh\\:mm} - {slot.EndTime:hh\\:mm}"
            })
            .ToList();

        AvailableTimes =
            new ObservableCollection<TimeSelectionItem>(times);

        SelectedTime =
            AvailableTimes.FirstOrDefault();
    }
    #endregion

    #region Availability

    private async Task LoadAvailabilityAsync(
        CancellationToken cancellationToken = default)
    {
        var member = _bookingSelectionState.Current.Stylist;

        if (member is null)
            return;

        if (member.BranchMemberServiceId <= 0)
            return;

        IsDatesLoading = true;

        try
        {
            var fromDate =
                new DateOnly(2026, 9, 1);

            var toDate =
                fromDate.AddDays(6);

            var request =
                new GetBookingAvailabilityRequest
                {
                    BranchMemberServiceId =
                        member.BranchMemberServiceId,

                    FromDate = fromDate,

                    ToDate = toDate,

                    SlotInterval =
                        TimeSpan.FromMinutes(15)
                };

            var result =
                await _bookingAvailabilityApiService
                    .GetAvailabilityAsync(
                        request,
                        cancellationToken);

            if (!result.IsSuccess ||
                result.Payload?.Dates is null)
            {
                _availability = null;

                AvailableDates.Clear();
                AvailableTimes.Clear();

                SelectedDate = null;
                SelectedTime = null;

                return;
            }

            _availability = result.Payload;

            BuildAvailableDates();
        }
        finally
        {
            IsDatesLoading = false;
        }
    }

    private void BuildAvailableDates()
    {
        if (_availability?.Dates is null ||
            _availability.Dates.Count == 0)
        {
            AvailableDates = new ObservableCollection<DateSelectionItem>();
            SelectedDate = null;
            SelectedDateTitle = string.Empty;
            SelectedTime = null;
            return;
        }

        var dates = _availability.Dates
            .Where(x => x.Slots is { Count: > 0 })
            .Select(date =>
            {
                var dateTime =
                    date.Date.ToDateTime(TimeOnly.MinValue);

                return new DateSelectionItem
                {
                    Date = dateTime,

                    DayName =
                        GetPersianDayName(
                            dateTime.DayOfWeek),

                    DayNumber =
                        GetPersianDayNumber(
                            dateTime),

                    MonthName =
                        GetPersianMonthName(
                            dateTime)
                };
            })
            .ToList();

        AvailableDates =
            new ObservableCollection<DateSelectionItem>(dates);

        var firstAvailableDate =
            AvailableDates.FirstOrDefault();

        if (firstAvailableDate is null)
        {
            SelectedDate = null;
            SelectedDateTitle = string.Empty;
            SelectedTime = null;
            return;
        }

        firstAvailableDate.IsSelected = true;

        SelectedDate = firstAvailableDate;
    }
    #endregion

    #region Persian Date Helpers

    private static readonly PersianCalendar PersianCalendar = new();

    private static string GetPersianDayName(
        DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه‌شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنجشنبه",
            DayOfWeek.Friday => "جمعه",

            _ => string.Empty
        };
    }

    private static string GetPersianDayNumber(
        DateTime date)
    {
        return PersianCalendar
            .GetDayOfMonth(date)
            .ToString(
                CultureInfo.InvariantCulture);
    }

    private static string GetPersianMonthName(
        DateTime date)
    {
        return PersianCalendar
            .GetMonth(date) switch
        {
            1 => "فروردین",
            2 => "اردیبهشت",
            3 => "خرداد",
            4 => "تیر",
            5 => "مرداد",
            6 => "شهریور",
            7 => "مهر",
            8 => "آبان",
            9 => "آذر",
            10 => "دی",
            11 => "بهمن",
            12 => "اسفند",

            _ => string.Empty
        };
    }

    #endregion

    #region Commands

    [RelayCommand]
    private Task GoToBookingConfirmationAsync()
    {
        var selection = _bookingSelectionState.Current;

        if (selection.Service is null)
            return Task.CompletedTask;

        if (selection.Branch is null)
            return Task.CompletedTask;

        if (selection.Stylist is null)
            return Task.CompletedTask;

        if (selection.Date is null)
            return Task.CompletedTask;

        if (selection.Time is null)
            return Task.CompletedTask;

        return _navigationService
            .GoToBookingConfirmationAsync();
    }

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService
            .GoBackAsync();
    }

    #endregion

    #region Initialization

    private bool _isInitialized;

    public async Task InitializeAsync(
        CancellationToken cancellationToken = default)
    {
        if (_isInitialized)
            return;

        _isInitialized = true;

        await LoadAvailabilityAsync(cancellationToken);
    }

    #endregion
}