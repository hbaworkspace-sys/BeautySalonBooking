using BeautySalonBooking.Maui.Features.Appointment.ViewModels;

namespace BeautySalonBooking.Maui.Features.Appointment.Views;

public partial class AppointmentsView : ContentView
{
    public AppointmentsView(AppointmentsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}