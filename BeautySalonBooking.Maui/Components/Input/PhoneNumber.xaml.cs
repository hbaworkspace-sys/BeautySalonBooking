using BeautySalonBooking.Maui.Common.Helpers;
using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.Components.Input;

public partial class PhoneNumberEntry : ContentView
{
    public event EventHandler? TextChanging;
    public event EventHandler? Completed;

    private bool _isUpdatingText;

    public PhoneNumberEntry()
    {
        InitializeComponent();

        UpdateGlyph();
        UpdateText();
        UpdateReturnType();
        UpdatePhoneType();
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
            typeof(PhoneNumberEntry),
            Icon.IconSymbols.PhoneIphone,
            propertyChanged: OnGlyphChanged);

    private static void OnGlyphChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntry control)
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
            typeof(PhoneNumberEntry),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnTextChanged);

    private static void OnTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntry control)
            control.UpdateText();
    }

    private void UpdateText()
    {
        if (_isUpdatingText)
            return;

        var value = NormalizeText(Text);

        if (value != Text)
        {
            SetValue(TextProperty, value);
            return;
        }

        SetEntryText(value);
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
            typeof(PhoneNumberEntry),
            ReturnType.Next,
            propertyChanged: OnReturnTypeChanged);

    private static void OnReturnTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntry control)
            control.UpdateReturnType();
    }

    private void UpdateReturnType()
    {
        MainEntry.ReturnType = ReturnType;
    }

    #endregion

    #region PhoneType

    public PhoneTypes PhoneType
    {
        get => (PhoneTypes)GetValue(PhoneTypeProperty);
        set => SetValue(PhoneTypeProperty, value);
    }

    public static readonly BindableProperty PhoneTypeProperty =
        BindableProperty.Create(
            nameof(PhoneType),
            typeof(PhoneTypes),
            typeof(PhoneNumberEntry),
            PhoneTypes.Mobile,
            propertyChanged: OnPhoneTypeChanged);

    private static void OnPhoneTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntry control)
            control.UpdatePhoneType();
    }

    private void UpdatePhoneType()
    {
        switch (PhoneType)
        {
            case PhoneTypes.Landline:
                Glyph = Icon.IconSymbols.Deskphone;
                MainEntry.Placeholder = "2134567890";
                break;

            case PhoneTypes.Mobile:
            default:
                Glyph = Icon.IconSymbols.PhoneIphone;
                MainEntry.Placeholder = "9123456789";
                break;
        }
    }

    public enum PhoneTypes
    {
        Mobile = 1,
        Landline = 2
    }

    #endregion

    #region Text Changed

    private void MainEntry_TextChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        if (_isUpdatingText)
            return;

        var input = e.NewTextValue ?? string.Empty;

        var normalized = NormalizeText(input);

        if (normalized.Length > 10)
            normalized = normalized[..10];

        if (normalized != Text)
            Text = normalized;

        if (MainEntry.Text != normalized)
            SetEntryText(normalized);

        TextChanging?.Invoke(this, e);

        if (normalized.Length == 10)
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }

    #endregion

    #region Normalization

    private static string NormalizeText(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var converter = new DigitConvertor();

        value = converter.ToEnglish(value);

        return new string(
            value.Where(char.IsDigit).ToArray());
    }

    #endregion

    #region Focus

    private void MainEntry_Focused(
        object? sender,
        FocusEventArgs e)
    {
        SelectAllText(MainEntry);
    }

    private void MainEntry_Unfocused(
        object? sender,
        FocusEventArgs e)
    {
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
        Text = NormalizeText(value);
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

    #region Completed

    private void MainEntry_Completed(
        object? sender,
        EventArgs e)
    {
        Completed?.Invoke(this, e);
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

    private void SetEntryText(string value)
    {
        _isUpdatingText = true;

        try
        {
            MainEntry.Text = value;
            MainEntry.CursorPosition = value.Length;
        }
        finally
        {
            _isUpdatingText = false;
        }
    }

    #endregion
}