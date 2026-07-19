namespace BeautySalonBooking.Maui.Features.AccountSetup;

public partial class SalonIntroductionPage : ContentPage
{
	public SalonIntroductionPage(SalonIntroductionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}