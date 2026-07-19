namespace BeautySalonBooking.Maui.Features.AccountSetup;

public partial class ChooseAccountTypePage : ContentPage
{
	public ChooseAccountTypePage(ChooseAccountTypeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}