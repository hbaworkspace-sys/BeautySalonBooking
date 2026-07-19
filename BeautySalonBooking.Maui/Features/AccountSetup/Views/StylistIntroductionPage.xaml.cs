namespace BeautySalonBooking.Maui.Features.AccountSetup;

public partial class StylistIntroductionPage : ContentPage
{
	public StylistIntroductionPage(StylistIntroductionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}