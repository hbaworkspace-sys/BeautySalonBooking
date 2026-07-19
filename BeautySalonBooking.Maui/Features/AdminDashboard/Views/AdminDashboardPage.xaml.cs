using BeautySalonBooking.Maui.Features.AdminDashboard.ViewModels;

namespace BeautySalonBooking.Maui.Features.AdminDashboard;

public partial class AdminDashboardPage : ContentPage
{
    public AdminDashboardPage(AdminDashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}