using BeautySalonBooking.Maui.Features.Profile.ViewModels;

namespace BeautySalonBooking.Maui.Features.Profile.Views;

public partial class DeleteAccountPage : ContentPage
{
    public DeleteAccountPage(DeleteAccountViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}