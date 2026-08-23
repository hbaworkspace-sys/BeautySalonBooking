namespace BeautySalonBooking.Maui.Components.DateAndTime.Models;

public class TimeSelectionItem
{
    private static int _nextId;

    public int Id { get; } = ++_nextId;

    public TimeSpan Time { get; set; }

    public string DisplayTime { get; set; } = string.Empty;

    public bool IsSelected { get; set; }
}
