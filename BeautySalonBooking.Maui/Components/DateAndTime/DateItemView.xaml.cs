using Microsoft.Maui.Controls.Shapes;

namespace BeautySalonBooking.Maui.Components.DateAndTime;

public partial class DateItemView : ContentView
{
    public DateItemView()
    {
        InitializeComponent();

        UpdateDayName();
        UpdateDayNumber();
        UpdateMonthName();
        UpdateSelection();
    }

    #region Date
    public string? DayName
    {
        get => (string?)GetValue(DayNameProperty);
        set => SetValue(DayNameProperty, value);
    }
    public static readonly BindableProperty DayNameProperty =
        BindableProperty.Create(
            nameof(DayName),
            typeof(string),
            typeof(DateItemView),
            default(string),
            propertyChanged: OnDatePropertyChanged);

    public string? DayNumber
    {
        get => (string?)GetValue(DayNumberProperty);
        set => SetValue(DayNumberProperty, value);
    }
    public static readonly BindableProperty DayNumberProperty =
        BindableProperty.Create(
            nameof(DayNumber),
            typeof(string),
            typeof(DateItemView),
            default(string),
            propertyChanged: OnDatePropertyChanged);

    public string? MonthName
    {
        get => (string?)GetValue(MonthNameProperty);
        set => SetValue(MonthNameProperty, value);
    }
    public static readonly BindableProperty MonthNameProperty =
        BindableProperty.Create(
            nameof(MonthName),
            typeof(string),
            typeof(DateItemView),
            default(string),
            propertyChanged: OnDatePropertyChanged);

    private static void OnDatePropertyChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        var control = (DateItemView)bindable;

        control.UpdateDayName();
        control.UpdateDayNumber();
        control.UpdateMonthName();
    }
    private void UpdateDayName()
    {
        DayNameLabel.Text = DayName;
    }

    private void UpdateDayNumber()
    {
        DayNumberLabel.Text = DayNumber;
    }

    private void UpdateMonthName()
    {
        MonthNameLabel.Text = MonthName;
    }
    #endregion

    #region Selection
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(DateItemView),
            false,
            propertyChanged: OnIsSelectedChanged);

    private static void OnIsSelectedChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        var control = (DateItemView)bindable;

        control.UpdateSelection();
    }
    #endregion

    #region Appearance
    public Brush NormalBackground
    {
        get => (Brush)GetValue(NormalBackgroundProperty);
        set => SetValue(NormalBackgroundProperty, value);
    }
    public static readonly BindableProperty NormalBackgroundProperty =
        BindableProperty.Create(
            nameof(NormalBackground),
            typeof(Brush),
            typeof(DateItemView),
            new SolidColorBrush(Colors.White),
            propertyChanged: OnAppearancePropertyChanged);

    public Brush SelectedBackground
    {
        get => (Brush)GetValue(SelectedBackgroundProperty);
        set => SetValue(SelectedBackgroundProperty, value);
    }
    public static readonly BindableProperty SelectedBackgroundProperty =
        BindableProperty.Create(
            nameof(SelectedBackground),
            typeof(Brush),
            typeof(DateItemView),
            new SolidColorBrush(Color.FromArgb("#F05C80")),
            propertyChanged: OnAppearancePropertyChanged);

    public Color NormalTextColor
    {
        get => (Color)GetValue(NormalTextColorProperty);
        set => SetValue(NormalTextColorProperty, value);
    }
    public static readonly BindableProperty NormalTextColorProperty =
        BindableProperty.Create(
            nameof(NormalTextColor),
            typeof(Color),
            typeof(DateItemView),
            Color.FromArgb("#6F666B"),
            propertyChanged: OnAppearancePropertyChanged);

    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }
    public static readonly BindableProperty SelectedTextColorProperty =
        BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(DateItemView),
            Colors.White,
            propertyChanged: OnAppearancePropertyChanged);

    public Brush BorderBrush
    {
        get => (Brush)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }
    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(DateItemView),
            new SolidColorBrush(Color.FromArgb("#E8E1E5")),
            propertyChanged: OnAppearancePropertyChanged);

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }
    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(DateItemView),
            1d,
            propertyChanged: OnAppearancePropertyChanged);

    public double SelectedBorderWidth
    {
        get => (double)GetValue(SelectedBorderWidthProperty);
        set => SetValue(SelectedBorderWidthProperty, value);
    }
    public static readonly BindableProperty SelectedBorderWidthProperty =
        BindableProperty.Create(
            nameof(SelectedBorderWidth),
            typeof(double),
            typeof(DateItemView),
            0d,
            propertyChanged: OnAppearancePropertyChanged);

    public float CornerRadius
    {
        get => (float)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(float),
            typeof(DateItemView),
            12f,
            propertyChanged: OnAppearancePropertyChanged);

    private static void OnAppearancePropertyChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        var control = (DateItemView)bindable;

        control.UpdateSelection();
    }
    private void UpdateSelection()
    {
        ContainerBorder.Background =
            IsSelected
                ? SelectedBackground
                : NormalBackground;

        ContainerBorder.Stroke =
            IsSelected
                ? SelectedBackground
                : BorderBrush;

        ContainerBorder.StrokeThickness =
            IsSelected
                ? SelectedBorderWidth
                : BorderWidth;

        var textColor =
            IsSelected
                ? SelectedTextColor
                : NormalTextColor;

        DayNameLabel.TextColor = textColor;
        DayNumberLabel.TextColor = textColor;
        MonthNameLabel.TextColor = textColor;

        ContainerBorder.StrokeShape =
            new RoundRectangle
            {
                CornerRadius = CornerRadius
            };
    }
    #endregion
}