using BeautySalonBooking.Maui.Common.Helpers;
using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.Components.Input;

public partial class NumericEntry : ContentView
{
    public event EventHandler? TextChanging;
    public event EventHandler? Completed;

    private bool _isUpdatingText;

    public NumericEntry()
    {
        InitializeComponent();

        UpdateGlyph();
        UpdateText();
        UpdatePlaceholder();
        UpdateReturnType();
        UpdateMaxLength();
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
            typeof(NumericEntry),
            Icon.IconSymbols.Person2,
            propertyChanged: OnGlyphChanged);

    private static void OnGlyphChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NumericEntry control)
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
            typeof(NumericEntry),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnTextChanged);

    private static void OnTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NumericEntry control)
            control.UpdateText();
    }

    private void UpdateText()
    {
        if (_isUpdatingText)
            return;

        SetEntryText(FormatForDisplay(Text));
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
            typeof(NumericEntry),
            string.Empty,
            propertyChanged: OnPlaceholderChanged);

    private static void OnPlaceholderChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NumericEntry control)
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
            typeof(NumericEntry),
            ReturnType.Next,
            propertyChanged: OnReturnTypeChanged);

    private static void OnReturnTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NumericEntry control)
            control.UpdateReturnType();
    }

    private void UpdateReturnType()
    {
        MainEntry.ReturnType = ReturnType;
    }

    #endregion

    #region MaxLength

    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    public static readonly BindableProperty MaxLengthProperty =
        BindableProperty.Create(
            nameof(MaxLength),
            typeof(int),
            typeof(NumericEntry),
            10,
            propertyChanged: OnMaxLengthChanged);

    private static void OnMaxLengthChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NumericEntry control)
            control.UpdateMaxLength();
    }

    private void UpdateMaxLength()
    {
        MainEntry.MaxLength = MaxLength;
    }

    #endregion

    #region NumericType

    public NumericMode NumericType
    {
        get => (NumericMode)GetValue(NumericTypeProperty);
        set => SetValue(NumericTypeProperty, value);
    }

    public static readonly BindableProperty NumericTypeProperty =
        BindableProperty.Create(
            nameof(NumericType),
            typeof(NumericMode),
            typeof(NumericEntry),
            NumericMode.None,
            propertyChanged: OnNumericTypeChanged);

    private static void OnNumericTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NumericEntry control)
            control.UpdateNumericType();
    }

    private void UpdateNumericType()
    {
        UpdateText();
    }

    public enum NumericMode
    {
        None = 0,
        Quantity = 1,
        Price = 2,
        Weight = 3
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

        // کاما فقط در حالت Price ممکن است داخل متن نمایشی وجود داشته باشد.
        input = input.Replace(",", string.Empty);

        // تبدیل اعداد فارسی به انگلیسی
        input = NormalizeDigits(input);

        // بررسی مقدار
        if (!IsValidInput(input))
        {
            SetEntryText(FormatForDisplay(e.OldTextValue));

            return;
        }

        // اعمال MaxLength روی مقدار خام
        if (MaxLength > 0 && input.Length > MaxLength)
        {
            input = input[..MaxLength];
        }

        // مقدار خام کنترل
        if (Text != input)
            Text = input;

        // مقدار نمایشی
        var displayText = FormatForDisplay(input);

        if (MainEntry.Text != displayText)
            SetEntryText(displayText);

        TextChanging?.Invoke(this, e);
    }

    #endregion

    #region Input Validation

    private string NormalizeDigits(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var converter = new DigitConvertor();

        return converter.ToEnglish(value);
    }

    private bool IsValidInput(string value)
    {
        if (string.IsNullOrEmpty(value))
            return true;

        return NumericType switch
        {
            NumericMode.None =>
                IsInteger(value),

            NumericMode.Quantity =>
                IsInteger(value),

            NumericMode.Price =>
                IsInteger(value),

            NumericMode.Weight =>
                IsDecimal(value),

            _ => false
        };
    }

    private static bool IsInteger(string value)
    {
        return ulong.TryParse(value, out _);
    }

    private static bool IsDecimal(string value)
    {
        return decimal.TryParse(value, out _);
    }

    #endregion

    #region Formatting

    private string FormatForDisplay(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        if (NumericType != NumericMode.Price)
            return value;

        return DigitConvertor.ToMoneyFormat(value);
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
        Text = value ?? string.Empty;
    }

    public string GetText()
    {
        return Text ?? string.Empty;
    }

    public int GetInt()
    {
        return int.TryParse(Text, out var value)
            ? value
            : 0;
    }

    public decimal GetDecimal()
    {
        return decimal.TryParse(Text, out var value)
            ? value
            : 0;
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

    #endregion
}