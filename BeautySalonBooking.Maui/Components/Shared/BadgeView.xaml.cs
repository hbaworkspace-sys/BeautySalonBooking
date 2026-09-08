using BeautySalonBooking.Maui.Components.Shared.Enums;

namespace BeautySalonBooking.Maui.Components.Shared;

public partial class BadgeView : ContentView
{
    public BadgeView()
    {
        InitializeComponent();

        UpdateBadgeType();

        UpdateText();
        UpdateTextColor();
        UpdateBackground();
        UpdateFontSize();
        UpdateFontAttributes();
        UpdateCornerRadius();
        UpdatePadding();
        UpdateBorder();
        UpdateShadow();
    }

    #region Content
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(BadgeView),
            default(string),
            propertyChanged: OnTextChanged);

    private static void OnTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateText();
    }
    private void UpdateText()
    {
        TextLabel.Text = Text;
    }
    #endregion

    #region Appearance
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(BadgeView),
            Colors.White,
            propertyChanged: OnTextColorChanged);

    private static void OnTextColorChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateTextColor();
    }
    private void UpdateTextColor()
    {
        TextLabel.TextColor = TextColor;
    }

    public Brush? FillBrush
    {
        get => (Brush?)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }
    public static readonly BindableProperty FillBrushProperty =
        BindableProperty.Create(
            nameof(FillBrush),
            typeof(Brush),
            typeof(BadgeView),
            null,
            propertyChanged: OnBackgroundChanged);

    private static void OnBackgroundChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateBackground();
    }
    private void UpdateBackground()
    {
        ContainerBorder.Background = FillBrush;
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(BadgeView),
            12d,
            propertyChanged: OnFontSizeChanged);

    private static void OnFontSizeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateFontSize();
    }

    private void UpdateFontSize()
    {
        TextLabel.FontSize = FontSize;
    }

    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(
            nameof(FontAttributes),
            typeof(FontAttributes),
            typeof(BadgeView),
            FontAttributes.Bold,
            propertyChanged: OnFontAttributesChanged);
    private static void OnFontAttributesChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateFontAttributes();
    }

    private void UpdateFontAttributes()
    {
        TextLabel.FontAttributes = FontAttributes;
    }
    #endregion

    #region Border
    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(BadgeView),
            null,
            propertyChanged: OnBorderChanged);

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(BadgeView),
            0d,
            propertyChanged: OnBorderChanged);

    private static void OnBorderChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateBorder();
    }

    private void UpdateBorder()
    {
        ContainerBorder.Stroke =
            BorderBrush;

        ContainerBorder.StrokeThickness =
            BorderWidth;
    }
    #endregion

    #region Shape
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(BadgeView),
            new CornerRadius(99),
            propertyChanged: OnCornerRadiusChanged);


    private static void OnCornerRadiusChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateCornerRadius();
    }
    private void UpdateCornerRadius()
    {
        BadgeShape.CornerRadius =
            CornerRadius;
    }
    #endregion

    #region Padding
    public Thickness BorderPadding
    {
        get => (Thickness)GetValue(BorderPaddingProperty);
        set => SetValue(BorderPaddingProperty, value);
    }

    public static readonly BindableProperty BorderPaddingProperty =
        BindableProperty.Create(
            nameof(BorderPadding),
            typeof(Thickness),
            typeof(BadgeView),
            new Thickness(0),
            propertyChanged: OnPaddingChanged);

    private static void OnPaddingChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdatePadding();
    }

    private void UpdatePadding()
    {
        ContainerBorder.Padding =
            BorderPadding;
    }
    #endregion

    #region Shadow
    public Shadow? Shadow
    {
        get => (Shadow?)GetValue(ShadowProperty);
        set => SetValue(ShadowProperty, value);
    }

    public static readonly BindableProperty ShadowProperty =
        BindableProperty.Create(
            nameof(Shadow),
            typeof(Shadow),
            typeof(BadgeView),
            null,
            propertyChanged: OnShadowChanged);

    private static void OnShadowChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateShadow();
    }

    private void UpdateShadow()
    {
        ContainerBorder.Shadow =
            Shadow;
    }

    #endregion

    #region Badge Type

    public BadgeType BadgeType
    {
        get => (BadgeType)GetValue(BadgeTypeProperty);
        set => SetValue(BadgeTypeProperty, value);
    }

    public static readonly BindableProperty BadgeTypeProperty =
        BindableProperty.Create(
            nameof(BadgeType),
            typeof(BadgeType),
            typeof(BadgeView),
            BadgeType.None,
            propertyChanged: OnBadgeTypeChanged);

    private static void OnBadgeTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        ((BadgeView)bindable).UpdateBadgeType();
    }

    private void UpdateBadgeType()
    {
        switch (BadgeType)
        {
            case BadgeType.Popular:
                Text = "محبوب";
                TextColor = Colors.White;
                FillBrush = new SolidColorBrush(
                    Color.FromArgb("#F34F78"));
                break;

            case BadgeType.Discount:
                Text = "تخفیف";
                TextColor = Colors.White;
                FillBrush = new SolidColorBrush(
                    Color.FromArgb("#25C759"));
                break;

            case BadgeType.New:
                Text = "جدید";
                TextColor = Colors.White;
                FillBrush = new SolidColorBrush(
                    Color.FromArgb("#6C63FF"));
                break;

            case BadgeType.Special:
                Text = "پیشنهاد ویژه";
                TextColor = Colors.White;
                FillBrush = new SolidColorBrush(
                    Color.FromArgb("#F5A623"));
                break;
        }
    }
    #endregion
}