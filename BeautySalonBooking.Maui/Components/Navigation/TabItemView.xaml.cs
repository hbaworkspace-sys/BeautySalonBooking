using BeautySalonBooking.Maui.Components.Shared.Enums;
using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Navigation;

public partial class TabItemView : ContentView
{
    public TabItemView()
    {
        InitializeComponent();

        UpdateAll();
    }

    #region Layout
    public StackOrientation TabOrientation
    {
        get => (StackOrientation)GetValue(TabOrientationProperty);
        set => SetValue(TabOrientationProperty, value);
    }
    public static readonly BindableProperty TabOrientationProperty =
        BindableProperty.Create(
            nameof(TabOrientation),
            typeof(StackOrientation),
            typeof(TabItemView),
            StackOrientation.Vertical,
            propertyChanged: OnLayoutChanged);

    public Thickness ContentPadding
    {
        get => (Thickness)GetValue(ContentPaddingProperty);
        set => SetValue(ContentPaddingProperty, value);
    }
    public static readonly BindableProperty ContentPaddingProperty =
        BindableProperty.Create(
            nameof(ContentPadding),
            typeof(Thickness),
            typeof(TabItemView),
            new Thickness(0),
            propertyChanged: OnLayoutChanged);


    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }
    public static readonly BindableProperty SpacingProperty =
        BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(TabItemView),
            4d,
            propertyChanged: OnLayoutChanged);


    public LayoutOptions ContentHorizontalOptions
    {
        get => (LayoutOptions)GetValue(ContentHorizontalOptionsProperty);
        set => SetValue(ContentHorizontalOptionsProperty, value);
    }
    public static readonly BindableProperty ContentHorizontalOptionsProperty =
        BindableProperty.Create(
            nameof(ContentHorizontalOptions),
            typeof(LayoutOptions),
            typeof(TabItemView),
            LayoutOptions.Fill,
            propertyChanged: OnLayoutChanged);

    public LayoutOptions ContentVerticalOptions
    {
        get => (LayoutOptions)GetValue(ContentVerticalOptionsProperty);
        set => SetValue(ContentVerticalOptionsProperty, value);
    }
    public static readonly BindableProperty ContentVerticalOptionsProperty =
        BindableProperty.Create(
            nameof(ContentVerticalOptions),
            typeof(LayoutOptions),
            typeof(TabItemView),
            LayoutOptions.Fill,
            propertyChanged: OnLayoutChanged);

    private static void OnLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TabItemView control)
            control.UpdateLayout();
    }

    private void UpdateLayout()
    {
        MainLayout.Padding = ContentPadding;
        MainLayout.Spacing = Spacing;

        MainLayout.HorizontalOptions =
            ContentHorizontalOptions;

        MainLayout.VerticalOptions =
            ContentVerticalOptions;

        MainLayout.Orientation =
            TabOrientation;
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
            typeof(TabItemView),
            false,
            propertyChanged: OnSelectionChanged);


    public Brush? NormalBackground
    {
        get => (Brush?)GetValue(NormalBackgroundProperty);
        set => SetValue(NormalBackgroundProperty, value);
    }

    public static readonly BindableProperty NormalBackgroundProperty =
        BindableProperty.Create(
            nameof(NormalBackground),
            typeof(Brush),
            typeof(TabItemView),
            null,
            propertyChanged: OnSelectionChanged);


    public Brush? SelectedBackground
    {
        get => (Brush?)GetValue(SelectedBackgroundProperty);
        set => SetValue(SelectedBackgroundProperty, value);
    }

    public static readonly BindableProperty SelectedBackgroundProperty =
        BindableProperty.Create(
            nameof(SelectedBackground),
            typeof(Brush),
            typeof(TabItemView),
            null,
            propertyChanged: OnSelectionChanged);


    public Color NormalIconColor
    {
        get => (Color)GetValue(NormalIconColorProperty);
        set => SetValue(NormalIconColorProperty, value);
    }

    public static readonly BindableProperty NormalIconColorProperty =
        BindableProperty.Create(
            nameof(NormalIconColor),
            typeof(Color),
            typeof(TabItemView),
            Colors.Gray,
            propertyChanged: OnSelectionChanged);


    public Color SelectedIconColor
    {
        get => (Color)GetValue(SelectedIconColorProperty);
        set => SetValue(SelectedIconColorProperty, value);
    }

    public static readonly BindableProperty SelectedIconColorProperty =
        BindableProperty.Create(
            nameof(SelectedIconColor),
            typeof(Color),
            typeof(TabItemView),
            Colors.Pink,
            propertyChanged: OnSelectionChanged);

    public Color NormalTextColor
    {
        get => (Color)GetValue(NormalTextColorProperty);
        set => SetValue(NormalTextColorProperty, value);
    }

    public static readonly BindableProperty NormalTextColorProperty =
        BindableProperty.Create(
            nameof(NormalTextColor),
            typeof(Color),
            typeof(TabItemView),
            Colors.Gray,
            propertyChanged: OnSelectionChanged);


    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    public static readonly BindableProperty SelectedTextColorProperty =
        BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(TabItemView),
            Colors.Pink,
            propertyChanged: OnSelectionChanged);


    private static void OnSelectionChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TabItemView control)
            control.UpdateSelection();
    }


    private void UpdateSelection()
    {
        ContainerBorder.Background =
            IsSelected
                ? SelectedBackground ?? NormalBackground
                : NormalBackground;

        UpdateIcon();

        IconView.GlyphColor =
            IsSelected
                ? SelectedIconColor
                : NormalIconColor;

        UpdateText();
        UpdateIndicator();
    }
    #endregion


    #region Icon
    public string? NormalIcon
    {
        get => (string?)GetValue(NormalIconProperty);
        set => SetValue(NormalIconProperty, value);
    }
    public static readonly BindableProperty NormalIconProperty =
        BindableProperty.Create(
            nameof(NormalIcon),
            typeof(string),
            typeof(TabItemView),
            null,
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
            typeof(TabItemView),
            null,
            propertyChanged: OnIconChanged);

    public bool IsIconVisible
    {
        get => (bool)GetValue(IsIconVisibleProperty);
        set => SetValue(IsIconVisibleProperty, value);
    }
    public static readonly BindableProperty IsIconVisibleProperty =
        BindableProperty.Create(
            nameof(IsIconVisible),
            typeof(bool),
            typeof(TabItemView),
            false,
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
            typeof(TabItemView),
            24d,
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
            typeof(TabItemView),
            "MaterialIconsRnd",
            propertyChanged: OnIconChanged);

    public FontAttributes IconFontAttributes
    {
        get => (FontAttributes)GetValue(IconFontAttributesProperty);
        set => SetValue(IconFontAttributesProperty, value);
    }
    public static readonly BindableProperty IconFontAttributesProperty =
        BindableProperty.Create(
            nameof(IconFontAttributes),
            typeof(FontAttributes),
            typeof(TabItemView),
            FontAttributes.None,
            propertyChanged: OnIconChanged);

    public DisplayMode IconMode
    {
        get => (DisplayMode)GetValue(IconModeProperty);
        set => SetValue(IconModeProperty, value);
    }
    public static readonly BindableProperty IconModeProperty =
        BindableProperty.Create(
            nameof(IconMode),
            typeof(DisplayMode),
            typeof(TabItemView),
            DisplayMode.Glyph,
            propertyChanged: OnIconChanged);


    public ImageSource? NormalIconImage
    {
        get => (ImageSource?)GetValue(NormalIconImageProperty);
        set => SetValue(NormalIconImageProperty, value);
    }
    public static readonly BindableProperty NormalIconImageProperty =
        BindableProperty.Create(
            nameof(NormalIconImage),
            typeof(ImageSource),
            typeof(TabItemView),
            null,
            propertyChanged: OnIconChanged);

    public ImageSource? SelectedIconImage
    {
        get => (ImageSource?)GetValue(SelectedIconImageProperty);
        set => SetValue(SelectedIconImageProperty, value);
    }
    public static readonly BindableProperty SelectedIconImageProperty =
        BindableProperty.Create(
            nameof(SelectedIconImage),
            typeof(ImageSource),
            typeof(TabItemView),
            null,
            propertyChanged: OnIconChanged);


    private static void OnIconChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TabItemView control)
            control.UpdateIcon();
    }


    private void UpdateIcon()
    {
        IconContainer.IsVisible = IsIconVisible;

        if (!IsIconVisible)
            return;

        if (IconMode == DisplayMode.Image)
        {
            UpdateImageIcon();
            return;
        }

        UpdateGlyphIcon();
    }


    private void UpdateGlyphIcon()
    {
        IconView.DisplayMode = DisplayMode.Glyph;

        IconView.Glyph =
            IsSelected && !string.IsNullOrWhiteSpace(SelectedIcon)
                ? SelectedIcon
                : NormalIcon;

        IconView.GlyphSize =
            IconSize;

        IconView.GlyphFontFamily =
            IconFontFamily;

        IconView.GlyphColor =
            IsSelected
                ? SelectedIconColor
                : NormalIconColor;
    }


    private void UpdateImageIcon()
    {
        IconView.DisplayMode = DisplayMode.Image;

        IconView.ImageSource =
            IsSelected && SelectedIconImage is not null
                ? SelectedIconImage
                : NormalIconImage;

        IconView.ImageAspect =
            Aspect.AspectFit;

        IconView.ImageOpacity =
            1;
    }
    #endregion

    #region Text
    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(TabItemView),
            null,
            propertyChanged: OnTextChanged);

    public bool IsTextVisible
    {
        get => (bool)GetValue(IsTextVisibleProperty);
        set => SetValue(IsTextVisibleProperty, value);
    }
    public static readonly BindableProperty IsTextVisibleProperty =
        BindableProperty.Create(
            nameof(IsTextVisible),
            typeof(bool),
            typeof(TabItemView),
            true,
            propertyChanged: OnTextChanged);

    public double TextSize
    {
        get => (double)GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
    }
    public static readonly BindableProperty TextSizeProperty =
        BindableProperty.Create(
            nameof(TextSize),
            typeof(double),
            typeof(TabItemView),
            10d,
            propertyChanged: OnTextChanged);


    public string? TextFontFamily
    {
        get => (string?)GetValue(TextFontFamilyProperty);
        set => SetValue(TextFontFamilyProperty, value);
    }

    public static readonly BindableProperty TextFontFamilyProperty =
        BindableProperty.Create(
            nameof(TextFontFamily),
            typeof(string),
            typeof(TabItemView),
            "Sans",
            propertyChanged: OnTextChanged);


    public FontAttributes TextFontAttributes
    {
        get => (FontAttributes)GetValue(TextFontAttributesProperty);
        set => SetValue(TextFontAttributesProperty, value);
    }

    public static readonly BindableProperty TextFontAttributesProperty =
        BindableProperty.Create(
            nameof(TextFontAttributes),
            typeof(FontAttributes),
            typeof(TabItemView),
            FontAttributes.None,
            propertyChanged: OnTextChanged);


    public TextAlignment TextAlignment
    {
        get => (TextAlignment)GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }

    public static readonly BindableProperty TextAlignmentProperty =
        BindableProperty.Create(
            nameof(TextAlignment),
            typeof(TextAlignment),
            typeof(TabItemView),
            TextAlignment.Center,
            propertyChanged: OnTextChanged);


    private static void OnTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TabItemView control)
            control.UpdateText();
    }


    private void UpdateText()
    {
        TitleLabel.Text =
            Text;

        TitleLabel.IsVisible =
            IsTextVisible;

        TitleLabel.FontSize =
            TextSize;

        TitleLabel.FontFamily =
            TextFontFamily;

        TitleLabel.FontAttributes =
            TextFontAttributes;

        TitleLabel.HorizontalTextAlignment =
            TextAlignment;

        TitleLabel.TextColor =
            IsSelected
                ? SelectedTextColor
                : NormalTextColor;
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
            typeof(TabItemView),
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
            typeof(TabItemView),
            0d,
            propertyChanged: OnBorderChanged);


    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(TabItemView),
            0d,
            propertyChanged: OnBorderChanged);


    public Thickness BorderPadding
    {
        get => (Thickness)GetValue(BorderPaddingProperty);
        set => SetValue(BorderPaddingProperty, value);
    }

    public static readonly BindableProperty BorderPaddingProperty =
        BindableProperty.Create(
            nameof(BorderPadding),
            typeof(Thickness),
            typeof(TabItemView),
            new Thickness(0),
            propertyChanged: OnBorderChanged);


    public Shadow? BorderShadow
    {
        get => (Shadow?)GetValue(BorderShadowProperty);
        set => SetValue(BorderShadowProperty, value);
    }

    public static readonly BindableProperty BorderShadowProperty =
        BindableProperty.Create(
            nameof(BorderShadow),
            typeof(Shadow),
            typeof(TabItemView),
            null,
            propertyChanged: OnBorderChanged);


    private static void OnBorderChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TabItemView control)
            control.UpdateBorder();
    }


    private void UpdateBorder()
    {
        ContainerBorder.Padding =
            BorderPadding;

        ContainerBorder.Stroke =
            BorderBrush;

        ContainerBorder.StrokeThickness =
            BorderWidth;

        ContainerBorder.Shadow =
            BorderShadow;

        Shape.CornerRadius =
            new CornerRadius(CornerRadius);
    }

    #endregion


    #region Badge

    public bool IsBadgeVisible
    {
        get => (bool)GetValue(IsBadgeVisibleProperty);
        set => SetValue(IsBadgeVisibleProperty, value);
    }

    public static readonly BindableProperty IsBadgeVisibleProperty =
        BindableProperty.Create(
            nameof(IsBadgeVisible),
            typeof(bool),
            typeof(TabItemView),
            false,
            propertyChanged: OnBadgeChanged);


    public string? BadgeText
    {
        get => (string?)GetValue(BadgeTextProperty);
        set => SetValue(BadgeTextProperty, value);
    }

    public static readonly BindableProperty BadgeTextProperty =
        BindableProperty.Create(
            nameof(BadgeText),
            typeof(string),
            typeof(TabItemView),
            null,
            propertyChanged: OnBadgeChanged);


    public Brush? BadgeBackground
    {
        get => (Brush?)GetValue(BadgeBackgroundProperty);
        set => SetValue(BadgeBackgroundProperty, value);
    }

    public static readonly BindableProperty BadgeBackgroundProperty =
        BindableProperty.Create(
            nameof(BadgeBackground),
            typeof(Brush),
            typeof(TabItemView),
            null,
            propertyChanged: OnBadgeChanged);


    public Color BadgeTextColor
    {
        get => (Color)GetValue(BadgeTextColorProperty);
        set => SetValue(BadgeTextColorProperty, value);
    }

    public static readonly BindableProperty BadgeTextColorProperty =
        BindableProperty.Create(
            nameof(BadgeTextColor),
            typeof(Color),
            typeof(TabItemView),
            Colors.White,
            propertyChanged: OnBadgeChanged);


    public double BadgeFontSize
    {
        get => (double)GetValue(BadgeFontSizeProperty);
        set => SetValue(BadgeFontSizeProperty, value);
    }

    public static readonly BindableProperty BadgeFontSizeProperty =
        BindableProperty.Create(
            nameof(BadgeFontSize),
            typeof(double),
            typeof(TabItemView),
            7d,
            propertyChanged: OnBadgeChanged);


    public Thickness BadgePadding
    {
        get => (Thickness)GetValue(BadgePaddingProperty);
        set => SetValue(BadgePaddingProperty, value);
    }

    public static readonly BindableProperty BadgePaddingProperty =
        BindableProperty.Create(
            nameof(BadgePadding),
            typeof(Thickness),
            typeof(TabItemView),
            new Thickness(4, 1),
            propertyChanged: OnBadgeChanged);


    public double BadgeCornerRadius
    {
        get => (double)GetValue(BadgeCornerRadiusProperty);
        set => SetValue(BadgeCornerRadiusProperty, value);
    }

    public static readonly BindableProperty BadgeCornerRadiusProperty =
        BindableProperty.Create(
            nameof(BadgeCornerRadius),
            typeof(double),
            typeof(TabItemView),
            50d,
            propertyChanged: OnBadgeChanged);


    public Brush? BadgeBorderBrush
    {
        get => (Brush?)GetValue(BadgeBorderBrushProperty);
        set => SetValue(BadgeBorderBrushProperty, value);
    }

    public static readonly BindableProperty BadgeBorderBrushProperty =
        BindableProperty.Create(
            nameof(BadgeBorderBrush),
            typeof(Brush),
            typeof(TabItemView),
            null,
            propertyChanged: OnBadgeChanged);


    public double BadgeBorderWidth
    {
        get => (double)GetValue(BadgeBorderWidthProperty);
        set => SetValue(BadgeBorderWidthProperty, value);
    }

    public static readonly BindableProperty BadgeBorderWidthProperty =
        BindableProperty.Create(
            nameof(BadgeBorderWidth),
            typeof(double),
            typeof(TabItemView),
            0d,
            propertyChanged: OnBadgeChanged);


    public Thickness BadgeMargin
    {
        get => (Thickness)GetValue(BadgeMarginProperty);
        set => SetValue(BadgeMarginProperty, value);
    }

    public static readonly BindableProperty BadgeMarginProperty =
        BindableProperty.Create(
            nameof(BadgeMargin),
            typeof(Thickness),
            typeof(TabItemView),
            new Thickness(0),
            propertyChanged: OnBadgeChanged);


    public LayoutOptions BadgeHorizontalOptions
    {
        get => (LayoutOptions)GetValue(BadgeHorizontalOptionsProperty);
        set => SetValue(BadgeHorizontalOptionsProperty, value);
    }

    public static readonly BindableProperty BadgeHorizontalOptionsProperty =
        BindableProperty.Create(
            nameof(BadgeHorizontalOptions),
            typeof(LayoutOptions),
            typeof(TabItemView),
            LayoutOptions.End,
            propertyChanged: OnBadgeChanged);


    public LayoutOptions BadgeVerticalOptions
    {
        get => (LayoutOptions)GetValue(BadgeVerticalOptionsProperty);
        set => SetValue(BadgeVerticalOptionsProperty, value);
    }

    public static readonly BindableProperty BadgeVerticalOptionsProperty =
        BindableProperty.Create(
            nameof(BadgeVerticalOptions),
            typeof(LayoutOptions),
            typeof(TabItemView),
            LayoutOptions.Start,
            propertyChanged: OnBadgeChanged);


    public double BadgeMinimumWidth
    {
        get => (double)GetValue(BadgeMinimumWidthProperty);
        set => SetValue(BadgeMinimumWidthProperty, value);
    }

    public static readonly BindableProperty BadgeMinimumWidthProperty =
        BindableProperty.Create(
            nameof(BadgeMinimumWidth),
            typeof(double),
            typeof(TabItemView),
            18d,
            propertyChanged: OnBadgeChanged);


    public double BadgeMinimumHeight
    {
        get => (double)GetValue(BadgeMinimumHeightProperty);
        set => SetValue(BadgeMinimumHeightProperty, value);
    }

    public static readonly BindableProperty BadgeMinimumHeightProperty =
        BindableProperty.Create(
            nameof(BadgeMinimumHeight),
            typeof(double),
            typeof(TabItemView),
            18d,
            propertyChanged: OnBadgeChanged);


    private static void OnBadgeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TabItemView control)
            control.UpdateBadge();
    }


    private void UpdateBadge()
    {
        BadgeView.IsVisible =
            IsBadgeVisible && IsIconVisible;

        BadgeView.FillBrush =
            BadgeBackground;

        BadgeView.BorderBrush =
            BadgeBorderBrush;

        BadgeView.BorderWidth =
            BadgeBorderWidth;

        BadgeView.BorderPadding =
            BadgePadding;

        BadgeView.Margin =
            BadgeMargin;

        BadgeView.HorizontalOptions =
            BadgeHorizontalOptions;

        BadgeView.VerticalOptions =
            BadgeVerticalOptions;

        BadgeView.MinimumWidthRequest =
            BadgeMinimumWidth;

        BadgeView.MinimumHeightRequest =
            BadgeMinimumHeight;

        BadgeView.Text =
            BadgeText;

        BadgeView.TextColor =
            BadgeTextColor;

        BadgeView.FontSize =
            BadgeFontSize;

        BadgeView.CornerRadius =
            BadgeCornerRadius;
    }

    #endregion


    #region Selected Indicator

    public bool ShowSelectedIndicator
    {
        get => (bool)GetValue(ShowSelectedIndicatorProperty);
        set => SetValue(ShowSelectedIndicatorProperty, value);
    }

    public static readonly BindableProperty ShowSelectedIndicatorProperty =
        BindableProperty.Create(
            nameof(ShowSelectedIndicator),
            typeof(bool),
            typeof(TabItemView),
            false,
            propertyChanged: OnIndicatorChanged);


    public Brush? NormalIndicatorBrush
    {
        get => (Brush?)GetValue(NormalIndicatorBrushProperty);
        set => SetValue(NormalIndicatorBrushProperty, value);
    }
    public static readonly BindableProperty NormalIndicatorBrushProperty =
            BindableProperty.Create(
                nameof(NormalIndicatorBrush),
                typeof(Brush),
                typeof(TabItemView),
                new SolidColorBrush(Colors.DarkGray),
                propertyChanged: OnIndicatorChanged);

    public Brush? SelectedIndicatorBrush
    {
        get => (Brush?)GetValue(SelectedIndicatorBrushProperty);
        set => SetValue(SelectedIndicatorBrushProperty, value);
    }

    public static readonly BindableProperty SelectedIndicatorBrushProperty =
        BindableProperty.Create(
            nameof(SelectedIndicatorBrush),
            typeof(Brush),
            typeof(TabItemView),
            new SolidColorBrush(Colors.Gray),
            propertyChanged: OnIndicatorChanged);

    public double SelectedIndicatorHeight
    {
        get => (double)GetValue(SelectedIndicatorHeightProperty);
        set => SetValue(SelectedIndicatorHeightProperty, value);
    }

    public static readonly BindableProperty SelectedIndicatorHeightProperty =
        BindableProperty.Create(
            nameof(SelectedIndicatorHeight),
            typeof(double),
            typeof(TabItemView),
            2d,
            propertyChanged: OnIndicatorChanged);


    public double SelectedIndicatorCornerRadius
    {
        get => (double)GetValue(SelectedIndicatorCornerRadiusProperty);
        set => SetValue(SelectedIndicatorCornerRadiusProperty, value);
    }

    public static readonly BindableProperty SelectedIndicatorCornerRadiusProperty =
        BindableProperty.Create(
            nameof(SelectedIndicatorCornerRadius),
            typeof(double),
            typeof(TabItemView),
            10d,
            propertyChanged: OnIndicatorChanged);


    public Thickness SelectedIndicatorMargin
    {
        get => (Thickness)GetValue(SelectedIndicatorMarginProperty);
        set => SetValue(SelectedIndicatorMarginProperty, value);
    }

    public static readonly BindableProperty SelectedIndicatorMarginProperty =
        BindableProperty.Create(
            nameof(SelectedIndicatorMargin),
            typeof(Thickness),
            typeof(TabItemView),
            new Thickness(0),
            propertyChanged: OnIndicatorChanged);


    private static void OnIndicatorChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TabItemView control)
            control.UpdateIndicator();
    }
    private void UpdateIndicator()
    {
        SelectedIndicator.IsVisible = ShowSelectedIndicator;

        SelectedIndicator.Background =
            IsSelected ?
            SelectedIndicatorBrush : NormalIndicatorBrush;

        SelectedIndicator.HeightRequest =
            SelectedIndicatorHeight;

        SelectedIndicator.Margin =
            SelectedIndicatorMargin;

        IndicatorShape.CornerRadius =
            new CornerRadius(SelectedIndicatorCornerRadius);
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
            typeof(TabItemView),
            null);


    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(TabItemView),
            null);


    private void OnTapped(object sender, TappedEventArgs e)
    {
        if (Command is not null)
        {
            if (!Command.CanExecute(CommandParameter))
                return;

            Command.Execute(CommandParameter);
        }

        Tapped?.Invoke(this, e);
    }
    public event EventHandler<TappedEventArgs>? Tapped;

    #endregion


    #region General

    private void UpdateAll()
    {
        UpdateLayout();
        UpdateBorder();
        UpdateSelection();
        UpdateText();
        UpdateBadge();
        UpdateIndicator();
    }
    #endregion
}