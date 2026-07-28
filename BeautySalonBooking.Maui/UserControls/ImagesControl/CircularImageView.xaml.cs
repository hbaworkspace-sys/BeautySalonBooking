namespace BeautySalonBooking.Maui.UserControls.Images;

public partial class CircularImageView : ContentView
{
    public CircularImageView()
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
        BindableProperty.Create(nameof(Size), typeof(SizeMode), typeof(CircularImageView), SizeMode.None,
            propertyChanged: OnSizeChanged);
    private static void OnSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var type = (SizeMode)newValue;
        var control = (CircularImageView)bindable;

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
            BindableProperty.Create(nameof(ImageBorderSize), typeof(int), typeof(CircularImageView), 165);

    public int MainBorderSize
    {
        get => (int)GetValue(MainBorderSizeProperty);
        set => SetValue(MainBorderSizeProperty, value);
    }
    public static readonly BindableProperty MainBorderSizeProperty =
            BindableProperty.Create(nameof(MainBorderSize), typeof(int), typeof(CircularImageView), 200);

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CircularImageView), default(ImageSource));

    public bool IsGolwEffectVisible
    {
        get => (bool)GetValue(IsGolwEffectVisibleProperty);
        set => SetValue(IsGolwEffectVisibleProperty, value);
    }
    public static readonly BindableProperty IsGolwEffectVisibleProperty =
            BindableProperty.Create(nameof(IsGolwEffectVisible), typeof(bool), typeof(CircularImageView), true);

    public Thickness InternalMargin
    {
        get => (Thickness)GetValue(InternalMarginProperty);
        set => SetValue(InternalMarginProperty, value);
    }
    public static readonly BindableProperty InternalMarginProperty =
            BindableProperty.Create(nameof(InternalMargin), typeof(Thickness), typeof(CircularImageView), default(Thickness));
}