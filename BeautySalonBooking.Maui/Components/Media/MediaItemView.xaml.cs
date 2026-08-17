using BeautySalonBooking.Maui.Components.Shared.Enums;
using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Media;

public partial class MediaItemView : ContentView
{
    public MediaItemView()
    {
        InitializeComponent();

        UpdateMedia();
        UpdateTitle();
    }

    #region Media
    public DisplayMode DisplayMode
    {
        get => (DisplayMode)GetValue(DisplayModeProperty);
        set => SetValue(DisplayModeProperty, value);
    }

    public static readonly BindableProperty DisplayModeProperty =
        BindableProperty.Create(
            nameof(DisplayMode),
            typeof(DisplayMode),
            typeof(MediaItemView),
            DisplayMode.Image,
            propertyChanged: OnMediaChanged);


    public ImageSource? ImageSource
    {
        get =>
            (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(
            nameof(ImageSource),
            typeof(ImageSource),
            typeof(MediaItemView),
            default(ImageSource),
            propertyChanged: OnMediaChanged);


    public Thickness ImageMargin
    {
        get => (Thickness)GetValue(ImageMarginProperty);
        set => SetValue(ImageMarginProperty, value);
    }
    public static readonly BindableProperty ImageMarginProperty =
        BindableProperty.Create(
            nameof(ImageMargin),
            typeof(Thickness),
            typeof(MediaItemView),
            new Thickness(0),
            propertyChanged: OnMediaChanged);


    public Aspect ImageAspect
    {
        get => (Aspect)GetValue(ImageAspectProperty);
        set => SetValue(ImageAspectProperty, value);
    }
    public static readonly BindableProperty ImageAspectProperty =
        BindableProperty.Create(
            nameof(ImageAspect),
            typeof(Aspect),
            typeof(MediaItemView),
            Aspect.AspectFit,
            propertyChanged: OnMediaChanged);


    public double ImageOpacity
    {
        get => (double)GetValue(ImageOpacityProperty);
        set => SetValue(ImageOpacityProperty, value);
    }
    public static readonly BindableProperty ImageOpacityProperty =
        BindableProperty.Create(
            nameof(ImageOpacity),
            typeof(double),
            typeof(MediaItemView),
            1d,
            propertyChanged: OnMediaChanged);

    public Brush ImageBorderBrush
    {
        get => (Brush)GetValue(ImageBorderBrushProperty);
        set => SetValue(ImageBorderBrushProperty, value);
    }

    public static readonly BindableProperty ImageBorderBrushProperty =
        BindableProperty.Create(
            nameof(ImageBorderBrush),
            typeof(Brush),
            typeof(MediaItemView),
            new SolidColorBrush(Colors.Transparent),
            propertyChanged: OnMediaChanged);


    public double ImageBorderWidth
    {
        get => (double)GetValue(ImageBorderWidthProperty);
        set => SetValue(ImageBorderWidthProperty, value);
    }

    public static readonly BindableProperty ImageBorderWidthProperty =
        BindableProperty.Create(
            nameof(ImageBorderWidth),
            typeof(double),
            typeof(MediaItemView),
            0d,
            propertyChanged: OnMediaChanged);

    public string? Glyph
    {
        get => (string?)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }
    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(
            nameof(Glyph),
            typeof(string),
            typeof(MediaItemView),
            default(string),
            propertyChanged: OnMediaChanged);


    public double GlyphSize
    {
        get => (double)GetValue(GlyphSizeProperty);
        set => SetValue(GlyphSizeProperty, value);
    }
    public static readonly BindableProperty GlyphSizeProperty =
        BindableProperty.Create(
            nameof(GlyphSize),
            typeof(double),
            typeof(MediaItemView),
            30d,
            propertyChanged: OnMediaChanged);


    public Color GlyphColor
    {
        get => (Color)GetValue(GlyphColorProperty);
        set => SetValue(GlyphColorProperty, value);
    }

    public static readonly BindableProperty GlyphColorProperty =
        BindableProperty.Create(
            nameof(GlyphColor),
            typeof(Color),
            typeof(MediaItemView),
            Colors.White,
            propertyChanged: OnMediaChanged);

    public string GlyphFontFamily
    {
        get => (string)GetValue(GlyphFontFamilyProperty);
        set => SetValue(GlyphFontFamilyProperty, value);
    }

    public static readonly BindableProperty GlyphFontFamilyProperty =
        BindableProperty.Create(
            nameof(GlyphFontFamily),
            typeof(string),
            typeof(MediaItemView),
            "MaterialIconsOut",
            propertyChanged: OnMediaChanged);

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(MediaItemView),
            default(string),
            propertyChanged: OnMediaChanged);

    public double TextSize
    {
        get => (double)GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
    }

    public static readonly BindableProperty TextSizeProperty =
        BindableProperty.Create(
            nameof(TextSize),
            typeof(double),
            typeof(MediaItemView),
            24d,
            propertyChanged: OnMediaChanged);


    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(MediaItemView),
            Colors.White,
            propertyChanged: OnMediaChanged);


    public string TextFontFamily
    {
        get => (string)GetValue(TextFontFamilyProperty);
        set => SetValue(TextFontFamilyProperty, value);
    }

    public static readonly BindableProperty TextFontFamilyProperty =
        BindableProperty.Create(
            nameof(TextFontFamily),
            typeof(string),
            typeof(MediaItemView),
            string.Empty,
            propertyChanged: OnMediaChanged);


    public FontAttributes TextFontAttributes
    {
        get => (FontAttributes)GetValue(TextFontAttributesProperty);
        set => SetValue(TextFontAttributesProperty, value);
    }

    public static readonly BindableProperty TextFontAttributesProperty =
        BindableProperty.Create(
            nameof(TextFontAttributes),
            typeof(FontAttributes),
            typeof(MediaItemView),
            FontAttributes.None,
            propertyChanged: OnMediaChanged);


    private static void OnMediaChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is MediaItemView control)
            control.UpdateMedia();
    }

    private void UpdateMedia()
    {
        Media.DisplayMode = DisplayMode;

        Media.ImageSource = ImageSource;
        Media.ImageMargin = ImageMargin;
        Media.ImageAspect = ImageAspect;
        Media.ImageOpacity = ImageOpacity;
        Media.ImageBorderBrush = ImageBorderBrush;
        Media.ImageBorderWidth = ImageBorderWidth;

        Media.Glyph = Glyph;
        Media.GlyphSize = GlyphSize;
        Media.GlyphColor = GlyphColor;
        Media.GlyphFontFamily = GlyphFontFamily;

        Media.Text = Text;
        Media.TextSize = TextSize;
        Media.TextColor = TextColor;
        Media.TextFontFamily = TextFontFamily;
        Media.TextFontAttributes = TextFontAttributes;
    }
    #endregion

    #region Layout
    public Shape Shape
    {
        get => (Shape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    public static readonly BindableProperty ShapeProperty =
        BindableProperty.Create(
            nameof(Shape),
            typeof(Shape),
            typeof(MediaItemView),
            Shape.Circle,
            propertyChanged: OnLayoutChanged);


    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(MediaItemView),
            0d,
            propertyChanged: OnLayoutChanged);


    private static void OnLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is MediaItemView control)
            control.UpdateLayout();
    }

    private void UpdateLayout()
    {
        Media.Shape = Shape;
        Media.CornerRadius = CornerRadius;
    }

    #endregion

    #region Appearance

    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(MediaItemView),
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
            typeof(MediaItemView),
            0d,
            propertyChanged: OnAppearanceChanged);


    public Brush FillBrush
    {
        get => (Brush)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }

    public static readonly BindableProperty FillBrushProperty =
        BindableProperty.Create(
            nameof(FillBrush),
            typeof(Brush),
            typeof(MediaItemView),
            new SolidColorBrush(Colors.Transparent),
            propertyChanged: OnAppearanceChanged);


    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is MediaItemView control)
            control.UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        Media.BorderBrush = BorderBrush;
        Media.BorderWidth = BorderWidth;
        Media.FillBrush = FillBrush;
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
            typeof(MediaItemView),
            default(Shadow),
            propertyChanged: OnShadowChanged);

    private static void OnShadowChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is MediaItemView control)
            control.UpdateShadow();
    }

    private void UpdateShadow()
    {
        Media.Shadow = Shadow;
    }
    #endregion

    #region Title
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(MediaItemView),
            default(string),
            propertyChanged: OnTitleChanged);


    public double TitleFontSize
    {
        get => (double)GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }

    public static readonly BindableProperty TitleFontSizeProperty =
        BindableProperty.Create(
            nameof(TitleFontSize),
            typeof(double),
            typeof(MediaItemView),
            12d,
            propertyChanged: OnTitleChanged);


    public Color TitleTextColor
    {
        get => (Color)GetValue(TitleTextColorProperty);
        set => SetValue(TitleTextColorProperty, value);
    }

    public static readonly BindableProperty TitleTextColorProperty =
        BindableProperty.Create(
            nameof(TitleTextColor),
            typeof(Color),
            typeof(MediaItemView),
            Colors.Black,
            propertyChanged: OnTitleChanged);


    public string TitleFontFamily
    {
        get => (string)GetValue(TitleFontFamilyProperty);
        set => SetValue(TitleFontFamilyProperty, value);
    }

    public static readonly BindableProperty TitleFontFamilyProperty =
        BindableProperty.Create(
            nameof(TitleFontFamily),
            typeof(string),
            typeof(MediaItemView),
            string.Empty,
            propertyChanged: OnTitleChanged);


    public FontAttributes TitleFontAttributes
    {
        get => (FontAttributes)GetValue(TitleFontAttributesProperty);
        set => SetValue(TitleFontAttributesProperty, value);
    }

    public static readonly BindableProperty TitleFontAttributesProperty =
        BindableProperty.Create(
            nameof(TitleFontAttributes),
            typeof(FontAttributes),
            typeof(MediaItemView),
            FontAttributes.None,
            propertyChanged: OnTitleChanged);


    public TextAlignment TitleHorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(TitleHorizontalTextAlignmentProperty);
        set => SetValue(TitleHorizontalTextAlignmentProperty, value);
    }

    public static readonly BindableProperty TitleHorizontalTextAlignmentProperty =
        BindableProperty.Create(
            nameof(TitleHorizontalTextAlignment),
            typeof(TextAlignment),
            typeof(MediaItemView),
            TextAlignment.Center,
            propertyChanged: OnTitleChanged);


    public LineBreakMode TitleLineBreakMode
    {
        get => (LineBreakMode)GetValue(TitleLineBreakModeProperty);
        set => SetValue(TitleLineBreakModeProperty, value);
    }

    public static readonly BindableProperty TitleLineBreakModeProperty =
        BindableProperty.Create(
            nameof(TitleLineBreakMode),
            typeof(LineBreakMode),
            typeof(MediaItemView),
            LineBreakMode.TailTruncation,
            propertyChanged: OnTitleChanged);


    private static void OnTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is MediaItemView control)
            control.UpdateTitle();
    }

    private void UpdateTitle()
    {
        TitleLabel.Text = Title;
        TitleLabel.FontSize = TitleFontSize;
        TitleLabel.TextColor = TitleTextColor;
        TitleLabel.FontFamily = TitleFontFamily;
        TitleLabel.FontAttributes = TitleFontAttributes;
        TitleLabel.HorizontalTextAlignment = TitleHorizontalTextAlignment;
        TitleLabel.LineBreakMode = TitleLineBreakMode;
    }

    #endregion

    #region Media Size

    public double MediaWidthRequest
    {
        get => (double)GetValue(MediaWidthRequestProperty);
        set => SetValue(MediaWidthRequestProperty, value);
    }

    public static readonly BindableProperty MediaWidthRequestProperty =
        BindableProperty.Create(
            nameof(MediaWidthRequest),
            typeof(double),
            typeof(MediaItemView),
            -1d,
            propertyChanged: OnMediaSizeChanged);


    public double MediaHeightRequest
    {
        get => (double)GetValue(MediaHeightRequestProperty);
        set => SetValue(MediaHeightRequestProperty, value);
    }

    public static readonly BindableProperty MediaHeightRequestProperty =
        BindableProperty.Create(
            nameof(MediaHeightRequest),
            typeof(double),
            typeof(MediaItemView),
            -1d,
            propertyChanged: OnMediaSizeChanged);


    private static void OnMediaSizeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not MediaItemView control)
            return;

        control.UpdateMediaSize();
    }

    private void UpdateMediaSize()
    {
        Media.WidthRequest = MediaWidthRequest;
        Media.HeightRequest = MediaHeightRequest;
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
            typeof(MediaItemView),
            default(ICommand));


    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(MediaItemView),
            default(object));

    #endregion


    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public static readonly BindableProperty SpacingProperty =
        BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(MediaItemView),
            0d,
            propertyChanged: OnSpacingChanged);

    private static void OnSpacingChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is MediaItemView control)
            control.RootLayout.Spacing = (double)newValue;
    }

}