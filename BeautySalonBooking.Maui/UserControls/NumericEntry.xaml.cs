using BeautySalonBooking.Maui.Helper;
using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.UserControls;

public partial class NumericEntry : ContentView
{
    public event EventHandler TextChanging;
    public event EventHandler OnCompleted;

    public NumericEntry()
    {
        InitializeComponent();
    }

    public string Glyph
    {
        get { return (string)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }
    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(nameof(Glyph), typeof(string), typeof(NumericEntry), Icon.IconSymbols.Person2);

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(NumericEntry), default(string));

    public string PlaceHolder
    {
        get { return (string)GetValue(PlaceHolderProperty); }
        set { SetValue(PlaceHolderProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderProperty =
        BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(NumericEntry), default(string));

    public ReturnType ReturnTypeValue
    {
        get { return (ReturnType)GetValue(ReturnTypeValueProperty); }
        set { SetValue(ReturnTypeValueProperty, value); }
    }
    public static readonly BindableProperty ReturnTypeValueProperty =
        BindableProperty.Create(nameof(ReturnTypeValue), typeof(ReturnType), typeof(NumericEntry), ReturnType.Next);

    public NumericMode NumericType
    {
        get { return (NumericMode)GetValue(NumericTypeProperty); }
        set { SetValue(NumericTypeProperty, value); }
    }
    public static readonly BindableProperty NumericTypeProperty =
        BindableProperty.Create(nameof(NumericType), typeof(NumericMode), typeof(NumericEntry), NumericMode.None);

    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }
    public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(NumericEntry), 10);

    public int Text_int
    {
        get
        {
            if (string.IsNullOrEmpty(Text))
                return 0;
            else
                return Convert.ToInt32(Text);
        }
    }

    public double Text_double
    {
        get
        {
            if (string.IsNullOrEmpty(Text))
                return 0;
            else

                return Convert.ToDouble(Text);
        }
    }

    public decimal Text_Decimal
    {
        get
        {
            if (string.IsNullOrEmpty(Text))
                return 0;
            else
                return Convert.ToDecimal(Text);
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
            if (NumericType == NumericMode.Price)
            {
                txt.Text = DigitConvertor.ToMoneyFormat(newtxt);
                txt.CursorPosition = txt.Text.Length;
            }
            else if (NumericType == NumericMode.Weight)
            {
                txt.CursorPosition = txt.Text.Length;
            }
            else if (NumericType == NumericMode.Quantity)
            {
                if (digitConvertor.IsNumeric_ulong(newtxt))
                    txt.Text = newtxt;
                else
                {
                    txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1);
                    txt.CursorPosition = txt.Text.Length;
                    return;
                }
            }
            else if (NumericType == NumericMode.None)
            {
                if (digitConvertor.IsNumeric_ulong(newtxt[newtxt.Length - 1].ToString()))
                    txt.Text = newtxt;
                else
                {
                    txt.Text = txt.Text.Remove(txt.Text.Length - 1, 1);
                    txt.CursorPosition = txt.Text.Length;
                    return;
                }
            }
            TextChanging?.Invoke(sender, e);
        }
        catch
        {
        }
        TextChanging?.Invoke(sender, e);
    }

    private void mainEntry_Focused(object sender, FocusEventArgs e)
    {
        lblPalceHolder.IsVisible = false;
        selectAllTexts(sender as Entry);
    }

    private void mainEntry_Unfocused(object sender, FocusEventArgs e)
    {
        border.StrokeThickness = 0;
        if (string.IsNullOrEmpty(Text))
            lblPalceHolder.IsVisible = true;
    }

    private void mainEntry_Completed(object sender, EventArgs e)
    {
        OnCompleted?.Invoke(sender, e);
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

    public void ResetControl()
    {
        Text = "";
        mainEntry.Unfocus();
    }

    public enum NumericMode
    {
        None = 0,     //اعداد غیر محاسباتی مثل شماره فاکتور یا کد ملی یا ....
        Quantity = 1, // صحیح مثل تعداد
        Price = 2,    // پول
        Weight = 3    // اعشار مثل وزن
    }
}