using BeautySalonBooking.Maui.Features.AdminDashboard.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.AdminDashboard.ViewModels
{
    public partial class AdminDashboardViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task SalonRequestsAsync()
        {
            await Shell.Current.GoToAsync(nameof(SalonRequestsPage));
        }

        [RelayCommand]
        private Task ReportsAsync()
        {
            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task StatisticsAsync()
        {
            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task AppointmentsAsync()
        {
            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task StylistsAsync()
        {
            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task CustomersAsync()
        {
            return Task.CompletedTask;
        }
    }
}
