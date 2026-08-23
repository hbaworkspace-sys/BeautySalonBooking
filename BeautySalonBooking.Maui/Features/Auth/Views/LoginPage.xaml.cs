using BeautySalonBooking.Maui.Features.Auth.ViewModels;

namespace BeautySalonBooking.Maui.Features.Auth.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}