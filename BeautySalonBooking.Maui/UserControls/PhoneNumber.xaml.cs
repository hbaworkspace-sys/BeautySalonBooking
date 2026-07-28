using BeautySalonBooking.Maui.Helper;
using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.UserControls;

public partial class PhoneNumberEntry : ContentView
{
    public event EventHandler TextChanging;
    public event EventHandler OnCompleted;

    public PhoneNumberEntry()
    {
        InitializeComponent();
    }

    public string Glyph
    {
        get { return (string)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }
    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(nameof(Glyph), typeof(string), typeof(PhoneNumberEntry), Icon.IconSymbols.PhoneIphone);

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(PhoneNumberEntry), default(string));

    public string PlaceHolder
    {
        get { return (string)GetValue(PlaceHolderProperty); }
        set { SetValue(PlaceHolderProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderProperty =
        BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(PhoneNumberEntry), "موبایل");

    public ReturnType ReturnTypeValue
    {
        get { return (ReturnType)GetValue(ReturnTypeValueProperty); }
        set { SetValue(ReturnTypeValueProperty, value); }
    }
    public static readonly BindableProperty ReturnTypeValueProperty =
        BindableProperty.Create(nameof(ReturnTypeValue), typeof(ReturnType), typeof(PhoneNumberEntry), ReturnType.Next);

    public enum PhoneTypes
    {
        Mobile = 1,
        Landline = 2
    }

    public PhoneTypes PhoneType
    {
        get { return (PhoneTypes)GetValue(PhoneTypeProperty); }
        set { SetValue(PhoneTypeProperty, value); }
    }
    public static readonly BindableProperty PhoneTypeProperty =
        BindableProperty.Create(nameof(PhoneType), typeof(PhoneTypes), typeof(PhoneNumberEntry), PhoneTypes.Mobile, propertyChanged: OnPhoneTypeChanged);
    private static void OnPhoneTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as PhoneNumberEntry;

        if (control != null)
        {
            if (control.PhoneType == PhoneTypes.Landline)
            {
                control.Glyph = Icon.IconSymbols.Deskphone;
                control.PlaceHolder = "ثابت";
            }
            else
            {
                control.Glyph = Icon.IconSymbols.PhoneIphone;
                control.PlaceHolder = "موبایل";
            }
        }
    }
    private void mainEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry txt = (Entry)sender;
        if (txt == null) return;

        DigitConvertor digitConvertor = new DigitConvertor();
        try
        {
            if (string.IsNullOrEmpty(txt.Text))
                return;
            string newtxt = digitConvertor.IsNumeric(txt.Text)
                            ? txt.Text
                            : digitConvertor.ToEnglish(txt.Text);

            if (digitConvertor.IsNumeric_ulong(newtxt[newtxt.Length - 1].ToString()))
                txt.Text = newtxt;
            else
            {
                txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1);
                txt.CursorPosition = txt.Text.Length;
                return;
            }

            TextChanging?.Invoke(sender, e);
        }
        catch
        {
        }
    }
    private void mainEntry_Focused(object sender, FocusEventArgs e)
    {
        lblPalceHolder.IsVisible = false;
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

    private void mainEntry_Unfocused(object sender, FocusEventArgs e)
    {
        border.StrokeThickness = 0;
        if (string.IsNullOrEmpty(Text))
            lblPalceHolder.IsVisible = true;
    }
    public void ResetControl()
    {
        Text = "";
        mainEntry.Unfocus();
    }

    private void mainEntry_Completed(object sender, EventArgs e)
    {
        OnCompleted?.Invoke(sender, e);
    }
}