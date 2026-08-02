namespace BeautySalonBooking.Maui.UserControls.Images;

public partial class CircularContentView : ContentView
{
    public CircularContentView()
    {
        InitializeComponent();
    }

    public enum SizeMode : byte
    {
        None = 0,
        Small = 1,
        Medium = 2,
        Large = 3
    }
    public SizeMode Size
    {
        get => (SizeMode)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
    public static readonly BindableProperty SizeProperty =
        BindableProperty.Create(nameof(Size), typeof(SizeMode), typeof(CircularContentView), SizeMode.None,
            propertyChanged: OnSizeChanged);
    private static void OnSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var type = (SizeMode)newValue;
        var control = (CircularContentView)bindable;

        if (control != null)
        {
            switch (type)
            {
                case SizeMode.Small:
                    break;

                case SizeMode.Medium:
                    control.MainBorderSize = 145;
                    control.ImageBorderSize = 110;
                    break;

                case SizeMode.Large:
                    control.MainBorderSize = 200;
                    control.ImageBorderSize = 165;
                    break;

                default:
                    break;
            }
        }
    }

    public int ImageBorderSize
    {
        get => (int)GetValue(ImageBorderSizeProperty);
        set => SetValue(ImageBorderSizeProperty, value);
    }
    public static readonly BindableProperty ImageBorderSizeProperty =
            BindableProperty.Create(nameof(ImageBorderSize), typeof(int), typeof(CircularContentView), 165);

    public int MainBorderSize
    {
        get => (int)GetValue(MainBorderSizeProperty);
        set => SetValue(MainBorderSizeProperty, value);
    }
    public static readonly BindableProperty MainBorderSizeProperty =
            BindableProperty.Create(nameof(MainBorderSize), typeof(int), typeof(CircularContentView), 200);

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CircularContentView), default(ImageSource));

    public bool IsGolwEffectVisible
    {
        get => (bool)GetValue(IsGolwEffectVisibleProperty);
        set => SetValue(IsGolwEffectVisibleProperty, value);
    }
    public static readonly BindableProperty IsGolwEffectVisibleProperty =
            BindableProperty.Create(nameof(IsGolwEffectVisible), typeof(bool), typeof(CircularContentView), true);

    public Thickness InternalMargin
    {
        get => (Thickness)GetValue(InternalMarginProperty);
        set => SetValue(InternalMarginProperty, value);
    }
    public static readonly BindableProperty InternalMarginProperty =
            BindableProperty.Create(nameof(InternalMargin), typeof(Thickness), typeof(CircularContentView), default(Thickness));

    public string Glyph
    {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }
    public static readonly BindableProperty GlyphProperty =
            BindableProperty.Create(nameof(Glyph), typeof(string), typeof(CircularContentView), default(string));

    public ContentMode ContentType
    {
        get => (ContentMode)GetValue(ContentTypeProperty);
        set => SetValue(ContentTypeProperty, value);
    }
    public static readonly BindableProperty ContentTypeProperty =
            BindableProperty.Create(nameof(ContentMode), typeof(ContentMode), typeof(CircularContentView), default(ContentMode),
                propertyChanged: OnContentChanged);
    private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CircularContentView)bindable;
        if (control != null)
        {
            switch (control.ContentType)
            {
                case ContentMode.Image:
                    control.ImageBox.IsVisible = true;
                    control.LblGlyph.IsVisible = false;
                    break;
                case ContentMode.Glyph:
                    control.ImageBox.IsVisible = false;
                    control.LblGlyph.IsVisible = true;
                    break;
                default:
                    break;
            }
        }
    }
    public enum ContentMode
    {
        Image = 0,
        Glyph = 1
    }

    public int FontSize
    {
        get => (int)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(int), typeof(CircularContentView), 120);

}