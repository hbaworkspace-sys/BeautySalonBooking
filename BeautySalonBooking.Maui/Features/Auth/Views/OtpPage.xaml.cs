using BeautySalonBooking.Maui.Features.Auth.ViewModels;

namespace BeautySalonBooking.Maui.Features.Auth.Views;

public partial class OtpPage : ContentPage
{
    public OtpPage(OtpViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void txtOtp_OnCompleted(object sender, EventArgs e)
    {
        if (BindingContext is OtpViewModel vm)
        {
            if (vm.VerifyOtpCommand.CanExecute(null))
                await vm.VerifyOtpCommand.ExecuteAsync(null);
        }
    }
}