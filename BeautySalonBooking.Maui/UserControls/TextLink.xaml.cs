using System.Windows.Input;
using System.Xml;

namespace BeautySalonBooking.Maui.UserControls;

public partial class TextLink : ContentView
{
    public TextLink()
    {
        InitializeComponent();
        UnderLineBox.WidthRequest = Button.Width;
    }
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(TextLink), "Empty");

    public ICommand HCommand
    {
        get { return (ICommand)GetValue(HCommandProperty); }
        set { SetValue(HCommandProperty, value); }
    }
    public static readonly BindableProperty HCommandProperty =
        BindableProperty.Create(nameof(HCommand), typeof(ICommand), typeof(TextLink), null);

    private async void Button_Clicked(object sender, EventArgs e)
    {
        UnderLineBox.IsVisible = true;
        UnderLineBox.WidthRequest = Button.Width-20;
        await Task.Delay(400);
        UnderLineBox.IsVisible = false;
    }
}