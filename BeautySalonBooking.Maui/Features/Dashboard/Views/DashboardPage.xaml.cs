using BeautySalonBooking.Maui.Features.Dashboard.ViewModels;

namespace BeautySalonBooking.Maui.Features.Dashboard.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}