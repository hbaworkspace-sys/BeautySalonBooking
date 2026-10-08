using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.Components.Input;

public partial class TextEntryWithTitle : ContentView
{
    public event EventHandler? TextChanging;
    public event EventHandler? Completed;

    public TextEntryWithTitle()
    {
        InitializeComponent();

        UpdateGlyph();
        UpdateText();
        UpdatePlaceholder();
        UpdateReturnType();
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
            typeof(TextEntryWithTitle),
            Icon.IconSymbols.Person,
            propertyChanged: OnGlyphChanged);

    private static void OnGlyphChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntryWithTitle control)
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
            typeof(TextEntryWithTitle),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnTextChanged);

    private static void OnTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntryWithTitle control)
            control.UpdateText();
    }

    private void UpdateText()
    {
        if (MainEntry.Text != Text)
            MainEntry.Text = Text;
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(TextEntryWithTitle), default(string), propertyChanged: OnTitleChanged);

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is TextEntryWithTitle control)
        {
            control.UpdateTitle();
        }
    }
    private void UpdateTitle()
    {
        TitleLable.Text = Title;
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
            typeof(TextEntryWithTitle),
            string.Empty,
            propertyChanged: OnPlaceholderChanged);

    private static void OnPlaceholderChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntryWithTitle control)
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
            typeof(TextEntryWithTitle),
            ReturnType.Next,
            propertyChanged: OnReturnTypeChanged);

    private static void OnReturnTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntryWithTitle control)
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

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        MainEntry.Focus();
    }

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
            typeof(TextEntryWithTitle),
            false,
            propertyChanged: OnStateChanged);

    private static void OnStateChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TextEntryWithTitle control)
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
            TitleLabelTextColor = TitleLable.TextColor,
            GlyphLabelTextColor = GlyphLabel.TextColor
        };

        if (!IsEnabled)
        {
            MainEntry.BackgroundColor = Color.FromArgb("#F5F2F4");
            MainEntry.TextColor = Color.FromArgb("#A29AA0");

            EntryBorder.BackgroundColor = Color.FromArgb("#F5F2F4");
            EntryBorder.Stroke = Color.FromArgb("#E4DDE1");

            TitleLable.TextColor = Color.FromArgb("#9A9398");
            GlyphLabel.TextColor = Color.FromArgb("#AAA2A8");

            return;
        }

        if (IsReadOnly)
        {
            MainEntry.BackgroundColor = Colors.White;
            MainEntry.TextColor = Color.FromArgb("#77717A");

            EntryBorder.BackgroundColor = Colors.White;
            EntryBorder.Stroke = Color.FromArgb("#E6E0E4");

            TitleLable.TextColor = Color.FromArgb("#8B848A");
            return;
        }

        // Normal
        MainEntry.BackgroundColor = normalState.MainEntryBackgroundColor;
        MainEntry.TextColor = normalState.MainEntryTextColor;

        EntryBorder.BackgroundColor = normalState.EntryBorderBackgroundColor;
        EntryBorder.Stroke = normalState.EntryBorderStroke;

        TitleLable.TextColor = normalState.TitleLabelTextColor;
        GlyphLabel.TextColor = normalState.GlyphLabelTextColor;
    }
    #endregion
}