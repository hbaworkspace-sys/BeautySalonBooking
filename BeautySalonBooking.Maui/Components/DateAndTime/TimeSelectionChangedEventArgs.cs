namespace BeautySalonBooking.Maui.Components.DateAndTime;

public class TimeSelectionChangedEventArgs : EventArgs
{
    public TimeSpan? PreviousTime { get; }

    public TimeSpan? SelectedTime { get; }

    public TimeSelectionChangedEventArgs(
        TimeSpan? previousTime,
        TimeSpan? selectedTime)
    {
        PreviousTime = previousTime;
        SelectedTime = selectedTime;
    }
}