using BeautySalonBooking.Maui.Features.Appointment.ViewModels;

namespace BeautySalonBooking.Maui.Features.Pages.Appointment;

public partial class AppointmentsPage : ContentPage
{
    public AppointmentsPage(AppointmentsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}