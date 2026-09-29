using System.Globalization;

namespace BeautySalonBooking.Maui.Common.Helpers;

public static class PersianDateHelper
{
    private static readonly PersianCalendar PersianCalendar = new();

    public static string GetDayName(DateOnly date)
    {
        return date.DayOfWeek switch
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

    public static string GetDayNumber(DateOnly date)
    {
        var dateTime = date.ToDateTime(TimeOnly.MinValue);

        return PersianCalendar
            .GetDayOfMonth(dateTime)
            .ToString(CultureInfo.InvariantCulture);
    }

    public static string GetMonthName(DateOnly date)
    {
        var dateTime = date.ToDateTime(TimeOnly.MinValue);

        return PersianCalendar.GetMonth(dateTime) switch
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

    public static string GetYear(DateOnly date)
    {
        var dateTime = date.ToDateTime(TimeOnly.MinValue);

        return PersianCalendar
            .GetYear(dateTime)
            .ToString(CultureInfo.InvariantCulture);
    }

    public static string FormatDate(DateOnly date)
    {
        return $"{GetDayName(date)} " +
               $"{GetDayNumber(date)} " +
               $"{GetMonthName(date)} " +
               $"{GetYear(date)}";
    }

    public static string FormatTime(
        TimeOnly startTime,
        TimeOnly endTime)
    {
        return $"ساعت {startTime:HH\\:mm} تا {endTime:HH\\:mm}";
    }

    public static string FormatTime(
    TimeOnly startTime)
    {
        return $"ساعت {startTime:HH\\:mm}";
    }

    public static string FormatPrice(decimal price)
    {
        return $"{price:#,0}".Replace(",", ".") + " تومان";
    }
}