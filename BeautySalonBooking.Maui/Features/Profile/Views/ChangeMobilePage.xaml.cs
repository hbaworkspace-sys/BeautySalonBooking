using BeautySalonBooking.Maui.Features.Profile.ViewModels;

namespace BeautySalonBooking.Maui.Features.Profile.Views;

public partial class ChangeMobilePage : ContentPage
{
    public ChangeMobilePage(ChangeMobileViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void PhoneEntry_Completed(object sender, EventArgs e)
    {
        if (BindingContext is ChangeMobileViewModel vm)
        {
            if (vm.RequestMobileChangeCommand.CanExecute(null))
                vm.RequestMobileChangeCommand.Execute(null);
        }
    }
}