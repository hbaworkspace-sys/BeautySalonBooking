using BeautySalonBooking.Maui.Features.Profile.ViewModels;

namespace BeautySalonBooking.Maui.Features.Profile.Views;

public partial class NotificationSettingsPage : ContentPage
{
    public NotificationSettingsPage(NotificationSettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}