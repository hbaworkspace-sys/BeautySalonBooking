using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.Components.Input;

public partial class TextEntry : ContentView
{
    public event EventHandler? TextChanging;
    public event EventHandler? Completed;

    public TextEntry()
    {
        InitializeComponent();

        UpdateGlyph();
        UpdateText();
        UpdatePlaceholder();
        UpdateReturnType();
    }

    #region Glyph

    public string Glyph
    {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(
            nameof(Glyph),
            typeof(string),
            typeof(TextEntry),
            Icon.IconSymbols.Person,
            propertyChanged: OnGlyphChanged);

    private static void OnGlyphChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntry control)
            control.UpdateGlyph();
    }

    private void UpdateGlyph()
    {
        GlyphLabel.Text = Glyph;
    }

    #endregion

    #region Text

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(TextEntry),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnTextChanged);

    private static void OnTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntry control)
            control.UpdateText();
    }

    private void UpdateText()
    {
        if (MainEntry.Text != Text)
            MainEntry.Text = Text;
    }

    #endregion

    #region Placeholder

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(TextEntry),
            string.Empty,
            propertyChanged: OnPlaceholderChanged);

    private static void OnPlaceholderChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntry control)
            control.UpdatePlaceholder();
    }

    private void UpdatePlaceholder()
    {
        MainEntry.Placeholder = Placeholder;
    }

    #endregion

    #region ReturnType

    public ReturnType ReturnType
    {
        get => (ReturnType)GetValue(ReturnTypeProperty);
        set => SetValue(ReturnTypeProperty, value);
    }

    public static readonly BindableProperty ReturnTypeProperty =
        BindableProperty.Create(
            nameof(ReturnType),
            typeof(ReturnType),
            typeof(TextEntry),
            ReturnType.Next,
            propertyChanged: OnReturnTypeChanged);

    private static void OnReturnTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntry control)
            control.UpdateReturnType();
    }

    private void UpdateReturnType()
    {
        MainEntry.ReturnType = ReturnType;
    }

    #endregion

    #region Events

    private void MainEntry_TextChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        if (Text != e.NewTextValue)
            Text = e.NewTextValue ?? string.Empty;

        TextChanging?.Invoke(this, e);
    }

    private void MainEntry_Completed(
        object? sender,
        EventArgs e)
    {
        Completed?.Invoke(this, e);
    }

    #endregion

    #region Focus

    private void MainEntry_Focused(
        object? sender,
        FocusEventArgs e)
    {
        SelectAllText(MainEntry);
    }

    public void Focus()
    {
        MainEntry.Focus();
    }

    public void Unfocus()
    {
        MainEntry.Unfocus();
    }

    #endregion

    #region Text Operations

    public void SetText(string? value)
    {
        Text = value ?? string.Empty;
    }

    public string GetText()
    {
        return Text ?? string.Empty;
    }

    public void Reset()
    {
        SetText(string.Empty);
        ClearError();
        Unfocus();
    }

    #endregion

    #region Error

    public void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }

    public void ClearError()
    {
        ErrorLabel.Text = string.Empty;
        ErrorLabel.IsVisible = false;
    }

    #endregion

    #region Platform

    private static void SelectAllText(Entry entry)
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

    #endregion
}