using BeautySalonBooking.Maui.Features.Dashboard.ViewModels;

namespace BeautySalonBooking.Maui.Features.Dashboard.Views;

public partial class DashboardView : ContentView
{
    public DashboardView(DashboardViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}