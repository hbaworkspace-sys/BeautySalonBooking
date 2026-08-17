using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Shared;

public partial class ActionIconView : ContentView
{
    public ActionIconView()
    {
        InitializeComponent();

        UpdateIcon();
        UpdateAppearance();
        UpdateLayout();
        UpdateCommand();
    }

    #region Icon
    public string? Icon
    {
        get => (string?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(ActionIconView),
            default(string),
            propertyChanged: OnIconChanged);


    public string? SelectedIcon
    {
        get => (string?)GetValue(SelectedIconProperty);
        set => SetValue(SelectedIconProperty, value);
    }
    public static readonly BindableProperty SelectedIconProperty =
        BindableProperty.Create(
            nameof(SelectedIcon),
            typeof(string),
            typeof(ActionIconView),
            default(string),
            propertyChanged: OnIconChanged);


    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    public static readonly BindableProperty IconSizeProperty =
        BindableProperty.Create(
            nameof(IconSize),
            typeof(double),
            typeof(ActionIconView),
            22d,
            propertyChanged: OnIconChanged);


    public Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }
    public static readonly BindableProperty IconColorProperty =
        BindableProperty.Create(
            nameof(IconColor),
            typeof(Color),
            typeof(ActionIconView),
            Colors.Black,
            propertyChanged: OnIconChanged);


    public Color SelectedIconColor
    {
        get => (Color)GetValue(SelectedIconColorProperty);
        set => SetValue(SelectedIconColorProperty, value);
    }

    public static readonly BindableProperty SelectedIconColorProperty =
        BindableProperty.Create(
            nameof(SelectedIconColor),
            typeof(Color),
            typeof(ActionIconView),
            Colors.Black,
            propertyChanged: OnIconChanged);


    public string IconFontFamily
    {
        get => (string)GetValue(IconFontFamilyProperty);
        set => SetValue(IconFontFamilyProperty, value);
    }

    public static readonly BindableProperty IconFontFamilyProperty =
        BindableProperty.Create(
            nameof(IconFontFamily),
            typeof(string),
            typeof(ActionIconView),
            "MaterialIconsOut",
            propertyChanged: OnIconChanged);


    private static void OnIconChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ActionIconView control)
            control.UpdateIcon();
    }


    private void UpdateIcon()
    {
        IconLabel.Text = IsSelected && !string.IsNullOrEmpty(SelectedIcon)
            ? SelectedIcon
            : Icon;

        IconLabel.FontSize = IconSize;

        IconLabel.TextColor = IsSelected
            ? SelectedIconColor
            : IconColor;

        IconLabel.FontFamily = IconFontFamily;
    }

    #endregion


    #region State
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(ActionIconView),
            false,
            propertyChanged: OnStateChanged);

    private static void OnStateChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ActionIconView control)
            control.UpdateIcon();
    }
    #endregion

    #region Layout
    public double TouchSize
    {
        get => (double)GetValue(TouchSizeProperty);
        set => SetValue(TouchSizeProperty, value);
    }
    public static readonly BindableProperty TouchSizeProperty =
        BindableProperty.Create(
            nameof(TouchSize),
            typeof(double),
            typeof(ActionIconView),
            25d,
            propertyChanged: OnLayoutChanged);

    public Thickness IconPadding
    {
        get => (Thickness)GetValue(IconPaddingProperty);
        set => SetValue(IconPaddingProperty, value);
    }

    public static readonly BindableProperty IconPaddingProperty =
        BindableProperty.Create(
            nameof(IconPadding),
            typeof(Thickness),
            typeof(ActionIconView),
            new Thickness(0),
            propertyChanged: OnLayoutChanged);


    private static void OnLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ActionIconView control)
            control.UpdateLayout();
    }

    private void UpdateLayout()
    {
        ContainerBorder.WidthRequest = TouchSize;
        ContainerBorder.HeightRequest = TouchSize;
        ContainerBorder.Padding = IconPadding;
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
            typeof(ActionIconView),
            new SolidColorBrush(Colors.Transparent),
            propertyChanged: OnAppearanceChanged);

    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(ActionIconView),
            new SolidColorBrush(Colors.Transparent),
            propertyChanged: OnAppearanceChanged);


    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(ActionIconView),
            0d,
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
            typeof(ActionIconView),
            0d,
            propertyChanged: OnAppearanceChanged);


    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ActionIconView control)
            control.UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        ContainerBorder.Background = FillBrush;
        ContainerBorder.Stroke = BorderBrush;
        ContainerBorder.StrokeThickness = BorderWidth;

        ActionShape.CornerRadius = new CornerRadius(CornerRadius);
    }
    #endregion


    #region Shadow
    public Shadow? IconShadow
    {
        get => (Shadow?)GetValue(IconShadowProperty);
        set => SetValue(IconShadowProperty, value);
    }

    public static readonly BindableProperty IconShadowProperty =
        BindableProperty.Create(
            nameof(IconShadow),
            typeof(Shadow),
            typeof(ActionIconView),
            default(Shadow),
            propertyChanged: OnShadowChanged);


    private static void OnShadowChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ActionIconView control)
            control.UpdateShadow();
    }


    private void UpdateShadow()
    {
        ContainerBorder.Shadow = IconShadow;
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
            typeof(ActionIconView),
            default(ICommand),
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
            typeof(ActionIconView),
            default(object),
            propertyChanged: OnCommandChanged);


    private static void OnCommandChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ActionIconView control)
            control.UpdateCommand();
    }

    private void UpdateCommand()
    {
        TapGesture.Command = Command;
        TapGesture.CommandParameter = CommandParameter;
    }
    #endregion
}