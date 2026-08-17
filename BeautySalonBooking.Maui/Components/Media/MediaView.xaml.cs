using BeautySalonBooking.Maui.Components.Shared.Enums;
using System.Windows.Input;
using MauiShapes = Microsoft.Maui.Controls.Shapes;

namespace BeautySalonBooking.Maui.Components.Media;

public partial class MediaView : ContentView
{
    public MediaView()
    {
        InitializeComponent();
        InitializeControl();
    }
    private void InitializeControl()
    {
        UpdateDisplayMode();
        UpdateContainer();
        UpdateImage();
        UpdateImageBorder();
        UpdateGlyph();
        UpdateText();
        UpdateShadow();
    }

    #region Content
    public DisplayMode DisplayMode
    {
        get => (DisplayMode)GetValue(DisplayModeProperty);
        set => SetValue(DisplayModeProperty, value);
    }
    public static readonly BindableProperty DisplayModeProperty =
        BindableProperty.Create(
            nameof(DisplayMode),
            typeof(DisplayMode),
            typeof(MediaView),
            DisplayMode.Image,
            propertyChanged: OnDisplayModeChanged);

    private static void OnDisplayModeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not MediaView control)
            return;
        control.UpdateDisplayMode();
    }
    private void UpdateDisplayMode()
    {
        ImageBorder.IsVisible = DisplayMode is DisplayMode.Image;
        GlyphLabel.IsVisible = DisplayMode is DisplayMode.Glyph;
        TextLabel.IsVisible = DisplayMode is DisplayMode.Text;
    }
    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(
            nameof(ImageSource),
            typeof(ImageSource),
            typeof(MediaView),
            default(ImageSource),
            propertyChanged: OnImageChanged);

    private static void OnImageChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not MediaView control)
            return;

        control.UpdateImage();
    }
    private void UpdateImage()
    {
        ImageView.Source = ImageSource;
        ImageView.Margin = ImageMargin;
        ImageView.Aspect = ImageAspect;
        ImageView.Opacity = ImageOpacity;
    }

    public string? Glyph
    {
        get => (string?)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }
    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(
            nameof(Glyph),
            typeof(string),
            typeof(MediaView),
            default(string),
            propertyChanged: OnGlyphChanged);

    private static void OnGlyphChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not MediaView control)
            return;

        control.UpdateGlyph();
    }
    private void UpdateGlyph()
    {
        GlyphLabel.Text = Glyph;
        GlyphLabel.FontSize = GlyphSize;
        GlyphLabel.TextColor = GlyphColor;
        GlyphLabel.FontFamily = GlyphFontFamily;

        System.Diagnostics.Debug.WriteLine(
            $"GLYPH => Text={GlyphLabel.Text} | " +
            $"Color={GlyphLabel.TextColor} | " +
            $"Font={GlyphLabel.FontFamily}");
    }
    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(MediaView),
            default(string),
            propertyChanged: OnTextChanged);

    private static void OnTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not MediaView control)
            return;

        control.UpdateText();
    }

    private void UpdateText()
    {
        TextLabel.Text = Text;
        TextLabel.FontSize = TextSize;
        TextLabel.TextColor = TextColor;
        TextLabel.FontFamily = TextFontFamily;
        TextLabel.FontAttributes = TextFontAttributes;
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
            typeof(MediaView),
            Shape.Circle,
            propertyChanged: OnShapeChanged);

    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(MediaView),
            0d,
            propertyChanged: OnShapeChanged);

    private static void OnShapeChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        if (bindable is not MediaView control)
            return;
        control.UpdateContainer();
        control.UpdateImageBorder();
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
            typeof(MediaView),
            new SolidColorBrush(Colors.Transparent),
            propertyChanged: OnAppearanceChanged);

    private static void OnAppearanceChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        if (bindable is not MediaView control)
            return;

        control.UpdateContainer();
    }

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }
    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(MediaView),
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
            typeof(MediaView),
             new SolidColorBrush(Colors.Transparent),
            propertyChanged: OnAppearanceChanged);
    #endregion

    #region Image
    public Thickness ImageMargin
    {
        get => (Thickness)GetValue(ImageMarginProperty);
        set => SetValue(ImageMarginProperty, value);
    }
    public static readonly BindableProperty ImageMarginProperty =
        BindableProperty.Create(
            nameof(ImageMargin),
            typeof(Thickness),
            typeof(MediaView),
            new Thickness(0),
            propertyChanged: OnImageChanged);

    public Aspect ImageAspect
    {
        get => (Aspect)GetValue(ImageAspectProperty);
        set => SetValue(ImageAspectProperty, value);
    }
    public static readonly BindableProperty ImageAspectProperty =
        BindableProperty.Create(
            nameof(ImageAspect),
            typeof(Aspect),
            typeof(MediaView),
            Aspect.AspectFit,
            propertyChanged: OnImageChanged);

    public double ImageOpacity
    {
        get => (double)GetValue(ImageOpacityProperty);
        set => SetValue(ImageOpacityProperty, value);
    }
    public static readonly BindableProperty ImageOpacityProperty =
        BindableProperty.Create(
            nameof(ImageOpacity),
            typeof(double),
            typeof(MediaView),
            1d,
            propertyChanged: OnImageChanged);



    // Image Border
    public Brush ImageBorderBrush
    {
        get => (Brush)GetValue(ImageBorderBrushProperty);
        set => SetValue(ImageBorderBrushProperty, value);
    }

    public static readonly BindableProperty ImageBorderBrushProperty =
        BindableProperty.Create(
            nameof(ImageBorderBrush),
            typeof(Brush),
            typeof(MediaView),
            new SolidColorBrush(Colors.Transparent),
            propertyChanged: OnImageBorderChanged);


    public double ImageBorderWidth
    {
        get => (double)GetValue(ImageBorderWidthProperty);
        set => SetValue(ImageBorderWidthProperty, value);
    }

    public static readonly BindableProperty ImageBorderWidthProperty =
        BindableProperty.Create(
            nameof(ImageBorderWidth),
            typeof(double),
            typeof(MediaView),
            0d,
            propertyChanged: OnImageBorderChanged);


    private static void OnImageBorderChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not MediaView control)
            return;

        control.UpdateImageBorder();
    }

    private void UpdateImageBorder()
    {
        ImageBorder.Stroke = ImageBorderBrush;
        ImageBorder.StrokeThickness = ImageBorderWidth;

        var shape = Shape switch
        {
            Shape.Circle => new MauiShapes.RoundRectangle
            {
                CornerRadius = new CornerRadius(9999)
            },

            Shape.Rectangle => new MauiShapes.RoundRectangle
            {
                CornerRadius = new CornerRadius(0)
            },

            Shape.RoundedRectangle => new MauiShapes.RoundRectangle
            {
                CornerRadius = new CornerRadius(CornerRadius)
            },

            _ => new MauiShapes.RoundRectangle
            {
                CornerRadius = new CornerRadius(0)
            }
        };

        ImageBorder.StrokeShape = shape;
    }
    #endregion

    #region Glyph
    public double GlyphSize
    {
        get => (double)GetValue(GlyphSizeProperty);
        set => SetValue(GlyphSizeProperty, value);
    }
    public static readonly BindableProperty GlyphSizeProperty =
        BindableProperty.Create(
            nameof(GlyphSize),
            typeof(double),
            typeof(MediaView),
            30d,
            propertyChanged: OnGlyphChanged);

    public Color GlyphColor
    {
        set
        {
            System.Diagnostics.Debug.WriteLine(
                $"[MEDIA SET] GlyphColor: {GlyphColor} -> {value}");

            SetValue(GlyphColorProperty, value);

            System.Diagnostics.Debug.WriteLine(
                $"[MEDIA SET AFTER] GlyphColor = {GlyphColor}");
        }
        get => (Color)GetValue(GlyphColorProperty);
        //set => SetValue(GlyphColorProperty, value);
    }
    public static readonly BindableProperty GlyphColorProperty =
        BindableProperty.Create(
            nameof(GlyphColor),
            typeof(Color),
            typeof(MediaView),
            Colors.White,
            propertyChanged: OnGlyphChanged);

    public string GlyphFontFamily
    {
        get => (string)GetValue(GlyphFontFamilyProperty);
        set => SetValue(GlyphFontFamilyProperty, value);
    }
    public static readonly BindableProperty GlyphFontFamilyProperty =
        BindableProperty.Create(
            nameof(GlyphFontFamily),
            typeof(string),
            typeof(MediaView),
            "MaterialIconsOut",
            propertyChanged: OnGlyphChanged);
    #endregion

    #region Text
    public double TextSize
    {
        get => (double)GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
    }
    public static readonly BindableProperty TextSizeProperty =
        BindableProperty.Create(
            nameof(TextSize),
            typeof(double),
            typeof(MediaView),
            24d,
            propertyChanged: OnTextChanged);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(MediaView),
            Colors.White,
            propertyChanged: OnTextChanged);

    public string TextFontFamily
    {
        get => (string)GetValue(TextFontFamilyProperty);
        set => SetValue(TextFontFamilyProperty, value);
    }
    public static readonly BindableProperty TextFontFamilyProperty =
        BindableProperty.Create(
            nameof(TextFontFamily),
            typeof(string),
            typeof(MediaView),
            string.Empty,
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
            typeof(MediaView),
            FontAttributes.None,
            propertyChanged: OnTextChanged);
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
            typeof(MediaView),
            default(Shadow),
            propertyChanged: OnShadowChanged);
    private static void OnShadowChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not MediaView control)
            return;

        control.UpdateShadow();
    }
    private void UpdateShadow()
    {
        ContainerBorder.Shadow = Shadow;
    }
    #endregion

    #region Glow
    public bool IsGlowVisible
    {
        get => (bool)GetValue(IsGlowVisibleProperty);
        set => SetValue(IsGlowVisibleProperty, value);
    }
    public static readonly BindableProperty IsGlowVisibleProperty =
        BindableProperty.Create(
            nameof(IsGlowVisible),
            typeof(bool),
            typeof(MediaView),
            false,
            propertyChanged: OnGlowChanged);

    public Brush? GlowBrush
    {
        get => (Brush?)GetValue(GlowBrushProperty);
        set => SetValue(GlowBrushProperty, value);
    }
    public static readonly BindableProperty GlowBrushProperty =
        BindableProperty.Create(
            nameof(GlowBrush),
            typeof(Brush),
            typeof(MediaView),
            new SolidColorBrush(Color.FromArgb("#44FFFFFF")),
            propertyChanged: OnGlowChanged);

    public double GlowPadding
    {
        get => (double)GetValue(GlowPaddingProperty);
        set => SetValue(GlowPaddingProperty, value);
    }
    public static readonly BindableProperty GlowPaddingProperty =
        BindableProperty.Create(
            nameof(GlowPadding),
            typeof(double),
            typeof(MediaView),
            10d,
            propertyChanged: OnGlowChanged);

    private static void OnGlowChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is MediaView control)
            control.UpdateContainer();
    }

    private void UpdateContainer()
    {
        var shape = Shape switch
        {
            Shape.Circle => new MauiShapes.RoundRectangle
            {
                CornerRadius = new CornerRadius(9999)
            },
            Shape.Rectangle => new MauiShapes.RoundRectangle
            {
                CornerRadius = new CornerRadius(0)
            },
            Shape.RoundedRectangle => new MauiShapes.RoundRectangle
            {
                CornerRadius = new CornerRadius(CornerRadius)
            },
            _ => new MauiShapes.RoundRectangle
            {
                CornerRadius = new CornerRadius(0)
            }
        };

        // Main Container
        ContainerBorder.StrokeShape = shape;
        ContainerBorder.Stroke = BorderBrush;
        ContainerBorder.StrokeThickness = BorderWidth;
        ContainerBorder.Background = FillBrush;

        // Glow
        GlowBorder.StrokeShape = shape;
        GlowBorder.IsVisible = IsGlowVisible;
        GlowBorder.Background = GlowBrush;
        GlowBorder.Margin = 0;

        ContainerBorder.Margin = IsGlowVisible
            ? new Thickness(GlowPadding)
            : new Thickness(0);
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
            typeof(MediaView),
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
            typeof(MediaView),
            default(object));
    #endregion

}