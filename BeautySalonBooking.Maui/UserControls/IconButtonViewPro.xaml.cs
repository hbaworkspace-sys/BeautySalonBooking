using System.Windows.Input;

namespace BeautySalonBooking.Maui.UserControls
{
    public partial class IconButtonViewPro : ContentView
    {
        public event EventHandler Clicked;
        public IconButtonViewPro()
        {
            InitializeComponent();
        }

        private async void OnTapped(object sender, EventArgs e)
        {
            // انیمیشن کلیک
            await Task.WhenAll(
                Border.ScaleTo(0.9, 150, Easing.CubicIn),
                Border.RotateTo(15, 200),
                Border.FadeTo(0.8, 200)
            );
            await Task.WhenAll(
                Border.ScaleTo(1.0, 300, Easing.CubicOut),
                Border.RotateTo(0, 300),
                Border.FadeTo(1.0, 300)
            );
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(IconButtonViewPro), default(string));

        public string IconGlyph
        {
            get => (string)GetValue(IconGlyphProperty);
            set => SetValue(IconGlyphProperty, value);
        }
        public static readonly BindableProperty IconGlyphProperty =
            BindableProperty.Create(nameof(IconGlyph), typeof(string), typeof(IconButtonViewPro), default(string));

        public Color ButtonColor
        {
            get => (Color)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }
        public static readonly BindableProperty ButtonColorProperty =
            BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(IconButtonViewPro), Colors.LightGray);

        public ICommand HCommand
        {
            get { return (ICommand)GetValue(HCommandProperty); }
            set { SetValue(HCommandProperty, value); }
        }
        public static readonly BindableProperty HCommandProperty =
            BindableProperty.Create(nameof(HCommand), typeof(ICommand), typeof(IconButtonViewPro), null);
    }
}