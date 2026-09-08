using BeautySalonBooking.Contracts.Appointment.Enums;
using BeautySalonBooking.Maui.Features.Appointment.ViewModels;

namespace BeautySalonBooking.Maui.Features.Appointment.Views;

public partial class AppointmentsView : ContentView
{
    private readonly AppointmentsViewModel _viewModel;

    public AppointmentsView(AppointmentsViewModel vm)
    {
        InitializeComponent();

        _viewModel = vm;
        BindingContext = vm;

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        await _viewModel.LoadAppointmentsAsync(AppointmentListType.All);
    }

}