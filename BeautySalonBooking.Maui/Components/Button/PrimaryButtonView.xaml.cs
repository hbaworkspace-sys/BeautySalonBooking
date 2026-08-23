using BeautySalonBooking.Maui.Components.Shared.Enums;
using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Button;

public partial class PrimaryButtonView : ContentView
{
    public PrimaryButtonView()
    {
        InitializeComponent();

        UpdateAll();
    }

    #region Content

    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(PrimaryButtonView),
            null,
            propertyChanged: OnContentChanged);

    private static void OnContentChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PrimaryButtonView control)
            control.UpdateContent();
    }

    private void UpdateContent()
    {
        Button.Text =
            Title;
    }

    #endregion


    #region Command

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(PrimaryButtonView),
            null,
            propertyChanged: OnCommandChanged);

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(PrimaryButtonView),
            null,
            propertyChanged: OnCommandChanged);

    private static void OnCommandChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PrimaryButtonView control)
            control.UpdateCommand();
    }

    private void UpdateCommand()
    {
        Button.Command =
            Command;

        Button.CommandParameter =
            CommandParameter;
    }

    #endregion


    #region Appearance

    public Brush? FillBrush
    {
        get => (Brush?)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }

    public static readonly BindableProperty FillBrushProperty =
        BindableProperty.Create(
            nameof(FillBrush),
            typeof(Brush),
            typeof(PrimaryButtonView),
            new SolidColorBrush(Color.FromArgb("#F05C80")),
            propertyChanged: OnAppearanceChanged);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(PrimaryButtonView),
            Colors.White,
            propertyChanged: OnAppearanceChanged);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(PrimaryButtonView),
            14d,
            propertyChanged: OnAppearanceChanged);

    public string? FontFamily
    {
        get => (string?)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    public static readonly BindableProperty FontFamilyProperty =
        BindableProperty.Create(
            nameof(FontFamily),
            typeof(string),
            typeof(PrimaryButtonView),
            "Sans",
            propertyChanged: OnAppearanceChanged);

    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(
            nameof(FontAttributes),
            typeof(FontAttributes),
            typeof(PrimaryButtonView),
            FontAttributes.Bold,
            propertyChanged: OnAppearanceChanged);

    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(PrimaryButtonView),
            9d,
            propertyChanged: OnAppearanceChanged);

    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PrimaryButtonView control)
            control.UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        UpdateAppearanceMode();

        Button.FontSize =
            FontSize;

        Button.FontFamily =
            FontFamily;

        Button.FontAttributes =
            FontAttributes;

        Button.CornerRadius =
            (int)CornerRadius;
    }

    private static Brush CreateDefaultBackground()
    {
        return new LinearGradientBrush(
            new GradientStopCollection
            {
                new GradientStop(Color.FromArgb("#EEF99BD3"), 0.0f),
                new GradientStop(Color.FromArgb("#EEFABAAC"), 1.0f)
            },
            new Point(0, 0),
            new Point(1, 1));
    }

    #endregion

    #region Appearance Mode

    public static readonly BindableProperty AppearanceModeProperty =
        BindableProperty.Create(
            nameof(AppearanceMode),
            typeof(AppearanceMode),
            typeof(PrimaryButtonView),
            AppearanceMode.Primary,
            propertyChanged: OnAppearanceModeChanged);

    public AppearanceMode AppearanceMode
    {
        get => (AppearanceMode)GetValue(AppearanceModeProperty);
        set => SetValue(AppearanceModeProperty, value);
    }

    private static void OnAppearanceModeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PrimaryButtonView control)
            control.UpdateAppearanceMode();
    }

    private void UpdateAppearanceMode()
    {
        switch (AppearanceMode)
        {
            case AppearanceMode.Primary:

                Button.Background = FillBrush;
                Button.TextColor = TextColor;

                Button.BorderColor = Colors.Transparent;
                Button.BorderWidth = 0;

                break;

            case AppearanceMode.Secondary:

                Button.Background = Colors.White;
                Button.TextColor = Color.FromArgb("#F05C80");

                Button.BorderColor = Color.FromArgb("#F05C80");
                Button.BorderWidth = 1;

                break;
        }
    }

    #endregion
    #region Layout

    public Thickness ContentPadding
    {
        get => (Thickness)GetValue(ContentPaddingProperty);
        set => SetValue(ContentPaddingProperty, value);
    }

    public static readonly BindableProperty ContentPaddingProperty =
        BindableProperty.Create(
            nameof(ContentPadding),
            typeof(Thickness),
            typeof(PrimaryButtonView),
            new Thickness(0),
            propertyChanged: OnLayoutChanged);

    private static void OnLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PrimaryButtonView control)
            control.UpdateLayout();
    }

    private void UpdateLayout()
    {
        Button.Padding =
            ContentPadding;
    }

    #endregion


    #region State

    public bool IsButtonEnabled
    {
        get => (bool)GetValue(IsButtonEnabledProperty);
        set => SetValue(IsButtonEnabledProperty, value);
    }

    public static readonly BindableProperty IsButtonEnabledProperty =
        BindableProperty.Create(
            nameof(IsButtonEnabled),
            typeof(bool),
            typeof(PrimaryButtonView),
            true,
            propertyChanged: OnStateChanged);

    private static void OnStateChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is PrimaryButtonView control)
            control.UpdateState();
    }

    private void UpdateState()
    {
        Button.IsEnabled =
            IsButtonEnabled;
    }

    #endregion


    #region General

    private void UpdateAll()
    {
        UpdateContent();
        UpdateCommand();
        UpdateAppearance();
        UpdateLayout();
        UpdateState();
    }
    #endregion
}