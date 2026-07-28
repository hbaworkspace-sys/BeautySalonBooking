using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.UserControls;

public partial class TextEntry : ContentView
{
    public event EventHandler TextChanging;
    public event EventHandler OnCompleted;

    public TextEntry()
    {
        InitializeComponent();
    }

    public string Glyph
    {
        get { return (string)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }
    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(nameof(Glyph), typeof(string), typeof(TextEntry), Icon.IconSymbols.Person);

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(TextEntry), default(string));

    public string PlaceHolder
    {
        get { return (string)GetValue(PlaceHolderProperty); }
        set { SetValue(PlaceHolderProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderProperty =
        BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(TextEntry), default(string));

    public ReturnType ReturnTypeValue
    {
        get { return (ReturnType)GetValue(ReturnTypeValueProperty); }
        set { SetValue(ReturnTypeValueProperty, value); }
    }
    public static readonly BindableProperty ReturnTypeValueProperty =
        BindableProperty.Create(nameof(ReturnTypeValue), typeof(ReturnType), typeof(TextEntry), ReturnType.Next);

    private void mainEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextChanging?.Invoke(sender, e);
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