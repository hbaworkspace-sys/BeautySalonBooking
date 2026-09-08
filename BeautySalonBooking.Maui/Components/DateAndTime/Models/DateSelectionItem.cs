namespace BeautySalonBooking.Maui.Components.DateAndTime.Models;

public class DateSelectionItem
{
    private static int _nextId;

    public int Id { get; } = ++_nextId;
    public DateTime Date { get; set; }

    public string DayName { get; set; } = string.Empty;

    public string DayNumber { get; set; } = string.Empty;

    public string MonthName { get; set; } = string.Empty;

    public bool IsSelected { get; set; }
}