using System.Collections;
using Microsoft.Maui.Controls.Shapes;

namespace BeautySalonBooking.Maui.Components.Input;

public partial class RadioButtonGroup : ContentView
{
    public RadioButtonGroup()
    {
        InitializeComponent();
        InitializeControl();
    }

    #region Initialization

    private void InitializeControl()
    {
        UpdateOptions();
    }

    #endregion

    #region Items

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(RadioButtonGroup),
            default(IEnumerable),
            propertyChanged: OnItemsSourceChanged);

    private static void OnItemsSourceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not RadioButtonGroup control)
            return;

        control.UpdateOptions();
    }

    #endregion

    #region Selection

    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(RadioButtonGroup),
            default(object),
            BindingMode.TwoWay,
            propertyChanged: OnSelectedItemChanged);

    private static void OnSelectedItemChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not RadioButtonGroup control)
            return;

        control.UpdateSelection();
    }

    #endregion

    #region Appearance

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    public static readonly BindableProperty SelectedColorProperty =
        BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.FromArgb("#F05C80"));

    public Color UnselectedColor
    {
        get => (Color)GetValue(UnselectedColorProperty);
        set => SetValue(UnselectedColorProperty, value);
    }

    public static readonly BindableProperty UnselectedColorProperty =
        BindableProperty.Create(
            nameof(UnselectedColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Colors.White);

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.FromArgb("#FCE1EA"));

    public Color SelectedBorderColor
    {
        get => (Color)GetValue(SelectedBorderColorProperty);
        set => SetValue(SelectedBorderColorProperty, value);
    }

    public static readonly BindableProperty SelectedBorderColorProperty =
        BindableProperty.Create(
            nameof(SelectedBorderColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.FromArgb("#F09AB8"));

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.FromArgb("#444444"));

    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    public static readonly BindableProperty SelectedTextColorProperty =
        BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Colors.White);

    public Color CheckColor
    {
        get => (Color)GetValue(CheckColorProperty);
        set => SetValue(CheckColorProperty, value);
    }

    public static readonly BindableProperty CheckColorProperty =
        BindableProperty.Create(
            nameof(CheckColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Colors.White);

    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(RadioButtonGroup),
            12d);

    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public static readonly BindableProperty SpacingProperty =
        BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(RadioButtonGroup),
            10d,
            propertyChanged: OnSpacingChanged);

    private static void OnSpacingChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not RadioButtonGroup control)
            return;

        control.OptionsContainer.Spacing = control.Spacing;
    }

    #endregion

    #region Update

    private void UpdateOptions()
    {
        OptionsContainer.Clear();

        if (ItemsSource is null)
            return;

        foreach (var item in ItemsSource)
        {
            if (item is not RadioButtonOption option)
                continue;

            var optionView = CreateOptionView(option);

            OptionsContainer.Add(optionView);
        }
    }

    private void UpdateSelection()
    {
        UpdateOptions();
    }

    #endregion

    #region Option

    private View CreateOptionView(RadioButtonOption option)
    {
        bool isSelected = Equals(SelectedItem, option.Value);

        var textLabel = new Label
        {
            Text = option.Text,
            FontSize = 14,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Fill,
            TextColor = isSelected
                ? SelectedTextColor
                : TextColor
        };

        var checkLabel = new Label
        {
            Text = "✓",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            TextColor = CheckColor,
            IsVisible = isSelected
        };

        var content = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            },
            ColumnSpacing = 8
        };

        content.Add(textLabel, 0, 0);
        content.Add(checkLabel, 1, 0);

        var border = new Border
        {
            Content = content,

            Padding = new Thickness(16, 10),

            Background = isSelected
                ? SelectedColor
                : UnselectedColor,

            Stroke = isSelected
                ? SelectedBorderColor
                : BorderColor,

            StrokeThickness = 1,

            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(CornerRadius)
            },

            HorizontalOptions = LayoutOptions.Fill
        };

        var tapGesture = new TapGestureRecognizer();

        tapGesture.Tapped += (_, _) =>
        {
            SelectedItem = option.Value;
        };

        border.GestureRecognizers.Add(tapGesture);

        return border;
    }

    #endregion
}