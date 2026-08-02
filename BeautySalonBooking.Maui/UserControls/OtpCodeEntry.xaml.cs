using BeautySalonBooking.Maui.Helper;

namespace BeautySalonBooking.Maui.UserControls;

public partial class OtpCodeEntry : ContentView
{
    public event EventHandler OnCompleted;
    private readonly Entry[] _entries;

    public OtpCodeEntry()
    {
        InitializeComponent();
        _entries =
          [
              txtDigit_1,
              txtDigit_2,
              txtDigit_3,
              txtDigit_4,
              txtDigit_5,
              txtDigit_6
          ];
        foreach (var entry in _entries)
        {
            entry.TextChanged += Entry_TextChanged;
            entry.Focused += Entry_Focused;
            entry.Completed += Entry_Completed;
        }
    }
    private void Entry_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not Entry entry)
            return;

        DigitConvertor converter = new();

        if (!string.IsNullOrEmpty(entry.Text))
        {
            string text = entry.Text;
            if (!converter.IsNumeric(text))
            {
                text = converter.ToEnglish(text);
            }

            if (!converter.IsNumeric_ulong(text))
            {
                entry.Text = "";
                return;
            }

            entry.Text = text;
            int index = Array.IndexOf(_entries, entry);

            if (index < _entries.Length - 1)
            {
                _entries[index + 1].Focus();
            }
            else
            {
                OnCompleted?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            int index = Array.IndexOf(_entries, entry);
            if (index > 0)
            {
                _entries[index - 1].Focus();
            }
        }
        try
        {
            if (!string.IsNullOrEmpty(entry.Text))
            {
                string text = entry.Text;
                if (!converter.IsNumeric(text))
                {
                    text = converter.ToEnglish(text);
                }

                if (!converter.IsNumeric_ulong(text))
                {
                    entry.Text = "";
                    return;
                }
            }
        }
        catch
        {
        }
        UpdateOtpCode();
    }

    public string OtpCode
    {
        get => (string)GetValue(OtpCodeProperty);
        set => SetValue(OtpCodeProperty, value);
    }
    public static readonly BindableProperty OtpCodeProperty =
        BindableProperty.Create(nameof(OtpCode), typeof(string), typeof(OtpCodeEntry), string.Empty, BindingMode.TwoWay);

    private void UpdateOtpCode()
    {
        OtpCode = string.Concat(_entries.Select(e => e.Text ?? string.Empty));
    }

    public void Clear()
    {
        foreach (var entry in _entries)
            entry.Text = string.Empty;

        _entries[0].Focus();
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry)
        {
            selectAllTexts(entry);
        }
    }

    private void selectAllTexts(Entry entry)
    {
#if ANDROID
        if (entry.Handler?.PlatformView is Android.Widget.EditText editText)
        {
            editText.Post(() => editText.SelectAll());
        }
#elif WINDOWS
    if (entry.Handler?.PlatformView is Microsoft.UI.Xaml.Controls.TextBox textBox)
    {
        textBox.SelectAll();
    }
#endif
    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        OnCompleted?.Invoke(sender, e);
    }

    public void FocusMe() => txtDigit_1.Focus();
}