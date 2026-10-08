using BeautySalonBooking.Maui.Features.Profile.ViewModels;

namespace BeautySalonBooking.Maui.Features.Profile.Views;

public partial class PrivacyPage : ContentPage
{
    public PrivacyPage(PrivacyViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}