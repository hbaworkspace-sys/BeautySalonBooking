namespace BeautySalonBooking.Maui.Components.Input;

public class RadioButtonOption
{
    public object? Value { get; set; }

    public string Text { get; set; } = string.Empty;

    public RadioButtonOption()
    {
    }

    public RadioButtonOption(object? value, string text)
    {
        Value = value;
        Text = text;
    }
}