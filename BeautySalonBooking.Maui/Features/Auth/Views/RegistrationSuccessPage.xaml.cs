using BeautySalonBooking.Maui.Features.Auth.ViewModels;

namespace BeautySalonBooking.Maui.Features.Auth.Views;

public partial class RegistrationSuccessPage : ContentPage
{
	public RegistrationSuccessPage(RegistrationSuccessViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}