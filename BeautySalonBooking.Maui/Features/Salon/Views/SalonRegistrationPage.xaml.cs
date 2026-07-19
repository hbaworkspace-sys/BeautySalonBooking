namespace BeautySalonBooking.Maui.Features.Salons;

public partial class SalonRegistrationPage : ContentPage
{
    public SalonRegistrationPage(SalonRegistrationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}