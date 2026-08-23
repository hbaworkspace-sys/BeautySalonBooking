using Microsoft.Maui.Controls.Shapes;

namespace BeautySalonBooking.Maui.Components.DateAndTime;

public partial class TimeItemView : ContentView
{
    public TimeItemView()
    {
        InitializeComponent();

        UpdateTime();
        UpdateSelection();
    }

    #region Time

    public string Time
    {
        get => (string)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }

    public static readonly BindableProperty TimeProperty =
        BindableProperty.Create(
            nameof(Time),
            typeof(string),
            typeof(TimeItemView),
            string.Empty,
            propertyChanged: OnTimeChanged);

    private static void OnTimeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not TimeItemView control)
            return;

        control.UpdateTime();
    }

    private void UpdateTime()
    {
        TimeLabel.Text = Time;
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
            typeof(TimeItemView),
            false,
            propertyChanged: OnSelectionChanged);

    private static void OnSelectionChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not TimeItemView control)
            return;

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

        TimeLabel.TextColor =
            IsSelected
                ? SelectedTextColor
                : NormalTextColor;

        SelectionIcon.IsVisible = IsSelected;

        ContainerBorder.StrokeShape =
            new RoundRectangle
            {
                CornerRadius = CornerRadius
            };
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
            typeof(TimeItemView),
            new SolidColorBrush(Color.FromArgb("#FFFFFF")),
            propertyChanged: OnAppearanceChanged);


    public Brush SelectedBackground
    {
        get => (Brush)GetValue(SelectedBackgroundProperty);
        set => SetValue(SelectedBackgroundProperty, value);
    }

    public static readonly BindableProperty SelectedBackgroundProperty =
        BindableProperty.Create(
            nameof(SelectedBackground),
            typeof(Brush),
            typeof(TimeItemView),
            new SolidColorBrush(Color.FromArgb("#F05C80")),
            propertyChanged: OnAppearanceChanged);


    public Color NormalTextColor
    {
        get => (Color)GetValue(NormalTextColorProperty);
        set => SetValue(NormalTextColorProperty, value);
    }

    public static readonly BindableProperty NormalTextColorProperty =
        BindableProperty.Create(
            nameof(NormalTextColor),
            typeof(Color),
            typeof(TimeItemView),
            Color.FromArgb("#6F666B"),
            propertyChanged: OnAppearanceChanged);


    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    public static readonly BindableProperty SelectedTextColorProperty =
        BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(TimeItemView),
            Colors.White,
            propertyChanged: OnAppearanceChanged);


    public Brush BorderBrush
    {
        get => (Brush)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(TimeItemView),
            new SolidColorBrush(Color.FromArgb("#E8E1E5")),
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
            typeof(TimeItemView),
            1d,
            propertyChanged: OnAppearanceChanged);


    public double SelectedBorderWidth
    {
        get => (double)GetValue(SelectedBorderWidthProperty);
        set => SetValue(SelectedBorderWidthProperty, value);
    }

    public static readonly BindableProperty SelectedBorderWidthProperty =
        BindableProperty.Create(
            nameof(SelectedBorderWidth),
            typeof(double),
            typeof(TimeItemView),
            0d,
            propertyChanged: OnAppearanceChanged);


    public float CornerRadius
    {
        get => (float)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(float),
            typeof(TimeItemView),
            12f,
            propertyChanged: OnAppearanceChanged);

    #endregion

    #region Appearance Update

    #region Appearance Update

    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not TimeItemView control)
            return;

        control.UpdateSelection();
    }

    #endregion


    #endregion
}