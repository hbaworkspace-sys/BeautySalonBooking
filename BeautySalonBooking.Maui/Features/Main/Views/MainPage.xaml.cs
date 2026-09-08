using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Home.Views;
using BeautySalonBooking.Maui.Features.Main.Models;
using BeautySalonBooking.Maui.Features.Main.ViewModels;
using BeautySalonBooking.Maui.Features.Appointment.Views;
using System.ComponentModel;

namespace BeautySalonBooking.Maui.Features.Main.Views;

public partial class MainPage : ContentPage, IQueryAttributable
{
    private readonly IViewService _viewService;

    public MainPage(
        MainViewModel vm,
        IViewService viewService)
    {
        InitializeComponent();

        BindingContext = vm;
        _viewService = viewService;

        vm.PropertyChanged += OnViewModelPropertyChanged;

        ShowView(vm.SelectedTab);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("SelectedTab", out var value) &&
            value is MainTab tab &&
            BindingContext is MainViewModel vm)
        {
            vm.SelectedTab = tab;
        }
    }

    private void OnViewModelPropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.SelectedTab) &&
            BindingContext is MainViewModel vm)
        {
            ShowView(vm.SelectedTab);
        }
    }

    private void ShowView(MainTab tab)
    {
        PageContainer.Content = tab switch
        {
            MainTab.Dashboard =>
                _viewService.GetView<HomeView>(),

            MainTab.Appointments =>
                _viewService.GetView<AppointmentsView>(),

            _ => null
        };
    }
}