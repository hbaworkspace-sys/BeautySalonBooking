using BeautySalonBooking.Maui.Common.Helpers;
using Microsoft.Maui.Graphics.Text;
using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.Components.Input;

public partial class PhoneNumberEntryWithTitle : ContentView
{
    private bool _isUpdatingText;

    public event EventHandler? TextChanging;
    public event EventHandler? Completed;

    public PhoneNumberEntryWithTitle()
    {
        InitializeComponent();

        UpdateGlyph();
        UpdateTitle();
        UpdateText();
        UpdateReturnType();
        UpdatePhoneType();
        UpdateCountryCode();
        UpdateVisualState();
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
            typeof(PhoneNumberEntryWithTitle),
            Icon.IconSymbols.PhoneIphone,
            propertyChanged: OnGlyphChanged);

    private static void OnGlyphChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntryWithTitle control)
            control.UpdateGlyph();
    }

    private void UpdateGlyph()
    {
        GlyphLabel.Text = Glyph;
    }
    #endregion

    #region Title
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(PhoneNumberEntryWithTitle),
            "شماره موبایل",
            propertyChanged: OnTitleChanged);

    private static void OnTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntryWithTitle control)
            control.UpdateTitle();
    }

    private void UpdateTitle()
    {
        TitleLabel.Text = Title;
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
            typeof(PhoneNumberEntryWithTitle),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnTextChanged);

    private static void OnTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntryWithTitle control)
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
            typeof(PhoneNumberEntryWithTitle),
            ReturnType.Next,
            propertyChanged: OnReturnTypeChanged);

    private static void OnReturnTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntryWithTitle control)
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
            typeof(PhoneNumberEntryWithTitle),
            PhoneTypes.Mobile,
            propertyChanged: OnPhoneTypeChanged);

    private static void OnPhoneTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntryWithTitle control)
            control.UpdatePhoneType();
    }

    private void UpdatePhoneType()
    {
        switch (PhoneType)
        {
            case PhoneTypes.Landline:
                Glyph = Icon.IconSymbols.Deskphone;
                Placeholder = "2134567890";
                break;

            case PhoneTypes.Mobile:
            default:
                Glyph = Icon.IconSymbols.PhoneIphone;
                Placeholder = "9123456789";
                break;
        }
    }

    public enum PhoneTypes
    {
        Mobile = 1,
        Landline = 2
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
            typeof(PhoneNumberEntryWithTitle),
            string.Empty,
            propertyChanged: OnPlaceholderChanged);

    private static void OnPlaceholderChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntryWithTitle control)
            control.UpdatePlaceholder();
    }

    private void UpdatePlaceholder()
    {
        MainEntry.Placeholder = Placeholder;
    }

    #endregion

    #region CountryCode

    public string CountryCode
    {
        get => (string)GetValue(CountryCodeProperty);
        set => SetValue(CountryCodeProperty, value);
    }

    public static readonly BindableProperty CountryCodeProperty =
        BindableProperty.Create(
            nameof(CountryCode),
            typeof(string),
            typeof(PhoneNumberEntryWithTitle),
            "+98",
            propertyChanged: OnCountryCodeChanged);

    private static void OnCountryCodeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntryWithTitle control)
            control.UpdateCountryCode();
    }

    private void UpdateCountryCode()
    {
        CountryCodeLabel.Text = CountryCode;
    }

    #endregion

    #region Text Changed

    private void MainEntry_TextChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        if (_isUpdatingText)
            return;

        var input = NormalizeText(e.NewTextValue);

        if (input.Length > 10)
            input = input[..10];

        if (Text != input)
            Text = input;

        if (MainEntry.Text != input)
            SetEntryText(input);

        TextChanging?.Invoke(this, e);

        if (input.Length == 10)
            Completed?.Invoke(this, EventArgs.Empty);
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

    private void TapGestureRecognizer_Tapped(
        object sender,
        TappedEventArgs e)
    {
        MainEntry.Focus();
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

    #region State

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public static readonly BindableProperty IsReadOnlyProperty =
        BindableProperty.Create(
            nameof(IsReadOnly),
            typeof(bool),
            typeof(PhoneNumberEntryWithTitle),
            false,
            propertyChanged: OnStateChanged);

    private static void OnStateChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PhoneNumberEntryWithTitle control)
            control.UpdateVisualState();
    }

    protected override void OnPropertyChanged(string? propertyName)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == IsEnabledProperty.PropertyName)
            UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        var normalState = new
        {
            MainEntryBackgroundColor = MainEntry.BackgroundColor,
            MainEntryTextColor = MainEntry.TextColor,
            EntryBorderBackgroundColor = EntryBorder.BackgroundColor,
            EntryBorderStroke = EntryBorder.Stroke,
            TitleLabelTextColor = TitleLabel.TextColor,
            GlyphLabelTextColor = GlyphLabel.TextColor
        };

        if (!IsEnabled)
        {
            MainEntry.BackgroundColor = Color.FromArgb("#F5F2F4");
            MainEntry.TextColor = Color.FromArgb("#A29AA0");

            EntryBorder.BackgroundColor = Color.FromArgb("#F5F2F4");
            EntryBorder.Stroke = Color.FromArgb("#E4DDE1");

            TitleLabel.TextColor = Color.FromArgb("#9A9398");
            GlyphLabel.TextColor = Color.FromArgb("#AAA2A8");

            return;
        }

        if (IsReadOnly)
        {
            MainEntry.BackgroundColor = Colors.White;
            MainEntry.TextColor = Color.FromArgb("#77717A");

            EntryBorder.BackgroundColor = Colors.White;
            EntryBorder.Stroke = Color.FromArgb("#E6E0E4");

            TitleLabel.TextColor = Color.FromArgb("#8B848A");
            return;
        }

        // Normal
        MainEntry.BackgroundColor = normalState.MainEntryBackgroundColor;
        MainEntry.TextColor = normalState.MainEntryTextColor;

        EntryBorder.BackgroundColor = normalState.EntryBorderBackgroundColor;
        EntryBorder.Stroke = normalState.EntryBorderStroke;

        TitleLabel.TextColor = normalState.TitleLabelTextColor;
        GlyphLabel.TextColor = normalState.GlyphLabelTextColor;
    }

    #endregion
}