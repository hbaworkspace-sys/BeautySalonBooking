using System.Windows.Input;

namespace BeautySalonBooking.Maui.UserControls;

public partial class DefaultButton : ContentView
{
	public event EventHandler OnClick;
	public DefaultButton()
	{
		InitializeComponent();
	}

	public string Title
	{
		get { return (string)GetValue(TitleProperty); }
		set { SetValue(TitleProperty, value); }
	}
	public static readonly BindableProperty TitleProperty =
		BindableProperty.Create(nameof(Title), typeof(string), typeof(DefaultButton), default(string));

	public ICommand HCommand
    {
		get { return (ICommand)GetValue(HCommandProperty); }
		set { SetValue(HCommandProperty, value); }
	}
	public static readonly BindableProperty HCommandProperty =
		BindableProperty.Create(nameof(HCommand), typeof(ICommand), typeof(DefaultButton), null);

	private void Button_Clicked(object sender, EventArgs e)
    {
		OnClick?.Invoke(sender, e);
    }
}