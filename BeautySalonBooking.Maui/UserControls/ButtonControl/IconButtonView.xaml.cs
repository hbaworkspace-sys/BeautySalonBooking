using BeautySalonBooking.Maui.UserControls.Images;

namespace BeautySalonBooking.Maui.UserControls.ButtonControl;

public partial class IconButtonView : ContentView
{
    public IconButtonView()
    {
        InitializeComponent();
    }


    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(IconButtonView), default(ImageSource));


}