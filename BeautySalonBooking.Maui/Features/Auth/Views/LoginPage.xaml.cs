using BeautySalonBooking.Maui.Features.Auth.ViewModels;

namespace BeautySalonBooking.Maui.Features.Auth.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void PhoneEntry_Completed(object sender, EventArgs e)
    {
        if (BindingContext is LoginViewModel vm)
        {
            if (vm.LoginCommand.CanExecute(null))
                vm.LoginCommand.Execute(null);
        }
    }

    RowDefinition x = new RowDefinition() { Height = 300 };
    private void PhoneEntry_Focused(object sender, FocusEventArgs e)
    {
        MainGrid.RowDefinitions.Add(x);
    }
    private void PhoneEntry_Unfocused(object sender, FocusEventArgs e)
    {
        MainGrid.RowDefinitions.Remove(x);
    }
}