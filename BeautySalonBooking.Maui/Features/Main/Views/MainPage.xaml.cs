using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Home.Views;
using BeautySalonBooking.Maui.Features.Main.Models;
using BeautySalonBooking.Maui.Features.Main.ViewModels;
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

        // هماهنگ کردن تب انتخاب‌شده با ViewModel
        MainTabBar.SelectedIndex = GetTabIndex(vm.SelectedTab);

        ShowView(vm.SelectedTab);
    }

    public void ApplyQueryAttributes(
        IDictionary<string, object> query)
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
        if (e.PropertyName != nameof(MainViewModel.SelectedTab) ||
            BindingContext is not MainViewModel vm)
        {
            return;
        }

        // به‌روزرسانی ظاهر TabBar
        MainTabBar.SelectedIndex = GetTabIndex(vm.SelectedTab);

        // به‌روزرسانی محتوای صفحه
        ShowView(vm.SelectedTab);
    }

    private static int GetTabIndex(MainTab tab)
    {
        return tab switch
        {
            MainTab.Messages => 0,
            MainTab.Search => 1,
            MainTab.Dashboard => 2,
            MainTab.Appointments => 3,
            MainTab.Profile => 4,
            _ => -1
        };
    }

    private void ShowView(MainTab tab)
    {
        PageContainer.Content = tab switch
        {
            MainTab.Dashboard =>
                _viewService.GetView<HomeView>(),

            //MainTab.Appointments =>
            //    _viewService.GetView<AppointmentsView>(),

            //MainTab.Profile =>
            //    _viewService.GetView<ProfileView>(),

            _ => null
        };
    }
}
