using BeautySalonBooking.Maui.Features.Auth.ViewModels;

namespace BeautySalonBooking.Maui.Features.Auth.Views;

public partial class OtpPage : ContentPage
{
    public OtpPage(OtpViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        txtOtp.FocusMe();
    }
}