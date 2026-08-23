using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Main.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Main.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private MainTab selectedTab = MainTab.Dashboard;

    public MainViewModel(IViewService viewService)
    {
    }

    [RelayCommand]
    private void SelectTab(MainTab tab)
    {
        SelectedTab = tab;
    }
}