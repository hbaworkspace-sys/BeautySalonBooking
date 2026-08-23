using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Components.DateAndTime.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BeautySalonBooking.Maui.Features.Booking.ViewModels;

public partial class DateTimeSelectionViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public DateTimeSelectionViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        LoadAvailableDates();
    }

    #region Dates

    [ObservableProperty]
    private ObservableCollection<DateSelectionItem> availableDates = new();

    [ObservableProperty]
    private DateSelectionItem? selectedDate;

    [ObservableProperty]
    private string selectedDateTitle = string.Empty;

    partial void OnSelectedDateChanged(DateSelectionItem? value)
    {
        if (value is null)
            return;

        SelectedDateTitle =
            $"تایم‌های خالی - {value.DayName} {value.DayNumber} {value.MonthName}";

        LoadAvailableTimes(value);
    }

    private void LoadAvailableDates()
    {
        AvailableDates = new ObservableCollection<DateSelectionItem>
        {
            new()
            {
                Date = new DateTime(2026, 7, 18),
                DayName = "پنجشنبه",
                DayNumber = "27",
                MonthName = "تیر"
            },

            new()
            {
                Date = new DateTime(2026, 7, 19),
                DayName = "جمعه",
                DayNumber = "28",
                MonthName = "تیر"
            },

            new()
            {
                Date = new DateTime(2026, 7, 20),
                DayName = "شنبه",
                DayNumber = "29",
                MonthName = "تیر",
                IsSelected = true
            },

            new()
            {
                Date = new DateTime(2026, 7, 21),
                DayName = "یکشنبه",
                DayNumber = "30",
                MonthName = "تیر"
            },

            new()
            {
                Date = new DateTime(2026, 7, 22),
                DayName = "دوشنبه",
                DayNumber = "31",
                MonthName = "تیر"
            },

            new()
            {
                Date = new DateTime(2026, 7, 23),
                DayName = "سه‌شنبه",
                DayNumber = "1",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 7, 24),
                DayName = "چهارشنبه",
                DayNumber = "2",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 7, 25),
                DayName = "پنجشنبه",
                DayNumber = "3",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 7, 26),
                DayName = "جمعه",
                DayNumber = "4",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 7, 27),
                DayName = "شنبه",
                DayNumber = "5",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 7, 28),
                DayName = "یکشنبه",
                DayNumber = "6",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 7, 29),
                DayName = "دوشنبه",
                DayNumber = "7",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 7, 30),
                DayName = "سه‌شنبه",
                DayNumber = "8",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 7, 31),
                DayName = "چهارشنبه",
                DayNumber = "9",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 8, 1),
                DayName = "پنجشنبه",
                DayNumber = "10",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 8, 2),
                DayName = "جمعه",
                DayNumber = "11",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 8, 3),
                DayName = "شنبه",
                DayNumber = "12",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 8, 4),
                DayName = "یکشنبه",
                DayNumber = "13",
                MonthName = "مرداد"
            },

            new()
            {
                Date = new DateTime(2026, 8, 5),
                DayName = "دوشنبه",
                DayNumber = "14",
                MonthName = "مرداد"
            }
        };

        SelectedDate =
            AvailableDates.FirstOrDefault(x => x.IsSelected);
    }

    #endregion


    #region Times

    [ObservableProperty]
    private ObservableCollection<TimeSelectionItem> availableTimes = new();

    [ObservableProperty]
    private TimeSelectionItem? selectedTime;

    partial void OnSelectedTimeChanged(TimeSelectionItem? value)
    {
        if (value is null)
            return;

        // در صورت نیاز بعداً اینجا منطق انتخاب تایم قرار می‌گیرد.
    }

    private void LoadAvailableTimes(DateSelectionItem date)
    {
        AvailableTimes = new ObservableCollection<TimeSelectionItem>
        {
            new()
            {
                Time = new TimeSpan(9, 0, 0),
                DisplayTime = "۰۹:۰۰"
            },

            new()
            {
                Time = new TimeSpan(10, 0, 0),
                DisplayTime = "۱۰:۰۰"
            },

            new()
            {
                Time = new TimeSpan(11, 0, 0),
                DisplayTime = "۱۱:۰۰"
            },

            new()
            {
                Time = new TimeSpan(12, 0, 0),
                DisplayTime = "۱۲:۰۰"
            },

            new()
            {
                Time = new TimeSpan(13, 0, 0),
                DisplayTime = "۱۳:۰۰"
            },

            new()
            {
                Time = new TimeSpan(14, 0, 0),
                DisplayTime = "۱۴:۰۰"
            },

            new()
            {
                Time = new TimeSpan(15, 30, 0),
                DisplayTime = "۱۵:۳۰",
                IsSelected = true
            },

            new()
            {
                Time = new TimeSpan(16, 30, 0),
                DisplayTime = "۱۶:۳۰"
            },

            new()
            {
                Time = new TimeSpan(17, 0, 0),
                DisplayTime = "۱۷:۰۰"
            },

            new()
            {
                Time = new TimeSpan(18, 0, 0),
                DisplayTime = "۱۸:۰۰"
            },

            new()
            {
                Time = new TimeSpan(19, 0, 0),
                DisplayTime = "۱۹:۰۰"
            },

            new()
            {
                Time = new TimeSpan(20, 0, 0),
                DisplayTime = "۲۰:۰۰"
            }
        };

        SelectedTime =
            AvailableTimes.FirstOrDefault(x => x.IsSelected);
    }
    #endregion

    [RelayCommand]
    private Task GoToBookingConfirmationAsync()
    {
        return _navigationService.GoToBookingConfirmationAsync();
    }
    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}