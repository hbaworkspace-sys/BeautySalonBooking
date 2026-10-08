using BeautySalonBooking.Maui.Features.Profile.ViewModels;

namespace BeautySalonBooking.Maui.Features.Profile.Views;

public partial class SecurityPage : ContentPage
{
    public SecurityPage(SecurityViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}