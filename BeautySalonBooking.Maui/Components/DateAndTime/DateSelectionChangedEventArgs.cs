namespace BeautySalonBooking.Maui.Components.DateAndTime;

public class DateSelectionChangedEventArgs : EventArgs
{
    public DateTime? PreviousDate { get; }

    public DateTime? SelectedDate { get; }

    public DateSelectionChangedEventArgs(
        DateTime? previousDate,
        DateTime? selectedDate)
    {
        PreviousDate = previousDate;
        SelectedDate = selectedDate;
    }
}