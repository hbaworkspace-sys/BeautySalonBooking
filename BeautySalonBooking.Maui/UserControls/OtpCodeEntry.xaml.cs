using BeautySalonBooking.Maui.Helper;

namespace BeautySalonBooking.Maui.UserControls;

public partial class OtpCodeEntry : ContentView
{
    public event EventHandler OnCompleted;
    public OtpCodeEntry()
    {
        InitializeComponent();
    }

    public char Digit_1
    {
        get { return (char)GetValue(Digit_1Property); }
        set { SetValue(Digit_1Property, value); }
    }
    public static readonly BindableProperty Digit_1Property =
        BindableProperty.Create(nameof(Digit_1), typeof(char), typeof(OtpCodeEntry), default(char) , propertyChanged:UpdateOtpCode);

    public char Digit_2
    {
        get { return (char)GetValue(Digit_2Property); }
        set { SetValue(Digit_2Property, value); }
    }
    public static readonly BindableProperty Digit_2Property =
        BindableProperty.Create(nameof(Digit_2), typeof(char), typeof(OtpCodeEntry), default(char), propertyChanged: UpdateOtpCode);

    public char Digit_3
    {
        get { return (char)GetValue(Digit_3Property); }
        set { SetValue(Digit_3Property, value); }
    }
    public static readonly BindableProperty Digit_3Property =
        BindableProperty.Create(nameof(Digit_3), typeof(char), typeof(OtpCodeEntry), default(char), propertyChanged: UpdateOtpCode);

    public char Digit_4
    {
        get { return (char)GetValue(Digit_4Property); }
        set { SetValue(Digit_4Property, value); }
    }
    public static readonly BindableProperty Digit_4Property =
        BindableProperty.Create(nameof(Digit_4), typeof(char), typeof(OtpCodeEntry), default(char), propertyChanged: UpdateOtpCode);

    public char Digit_5
    {
        get { return (char)GetValue(Digit_5Property); }
        set { SetValue(Digit_5Property, value); }
    }
    public static readonly BindableProperty Digit_5Property =
        BindableProperty.Create(nameof(Digit_5), typeof(char), typeof(OtpCodeEntry), default(char), propertyChanged: UpdateOtpCode);

    public char Digit_6
    {
        get { return (char)GetValue(Digit_6Property); }
        set { SetValue(Digit_6Property, value); }
    }
    public static readonly BindableProperty Digit_6Property =
        BindableProperty.Create(nameof(Digit_6), typeof(char), typeof(OtpCodeEntry), default(char), propertyChanged: UpdateOtpCode);

    public string OtpCode
    {
        get { return (string)GetValue(OtpCodeProperty); }
        set { SetValue(OtpCodeProperty, value); }
    }
    public static readonly BindableProperty OtpCodeProperty =
        BindableProperty.Create(nameof(OtpCode), typeof(string), typeof(OtpCodeEntry), "");

    private static void UpdateOtpCode(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (OtpCodeEntry)bindable;
        if (control != null)
            control.OtpCode = $"{control.Digit_1}{control.Digit_2}{control.Digit_3}{control.Digit_4}{control.Digit_5}{control.Digit_6}";
    }
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;
        if (entry == null) return;

        if (entry == txtDigit_1 && Digit_1 != '\0')
            txtDigit_2.Focus();

        else if (entry == txtDigit_2 && Digit_2 != '\0')
            txtDigit_3.Focus();

        else if (entry == txtDigit_3 && Digit_3 != '\0')
            txtDigit_4.Focus();

        else if (entry == txtDigit_4 && Digit_4 != '\0')
            txtDigit_5.Focus();

        else if (entry == txtDigit_5 && Digit_5 != '\0')
            txtDigit_6.Focus();

        else if (entry == txtDigit_6 && Digit_6 != '\0')
            Entry_Completed(sender, e);

        DigitConvertor digitConvertor = new DigitConvertor();

        try
        {
            if (string.IsNullOrEmpty(entry.Text))
                return;
            string newtxt = digitConvertor.IsNumeric(entry.Text)
                            ? entry.Text
                            : digitConvertor.ToEnglish(entry.Text);

            if (digitConvertor.IsNumeric_ulong(newtxt[newtxt.Length - 1].ToString()))
                entry.Text = newtxt;
            else
            {
                entry.Text = entry.Text.Remove(entry.Text.Length - 1, 1);
                entry.CursorPosition = entry.Text.Length;
                return;
            }
        }
        catch
        {
        }
    }
    public void ResetControl()
    {
        Digit_1 = '\0';
        Digit_2 = '\0';
        Digit_3 = '\0';
        Digit_4 = '\0';
        Digit_5 = '\0';
        Digit_6 = '\0';
        FocuseMe();
    }
    public void FocuseMe()
    {
        txtDigit_1.Focus();
    }
    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        selectAllTexts(sender as Entry);
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
}