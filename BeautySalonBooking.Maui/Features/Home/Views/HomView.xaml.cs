using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Auth.Services;
using BeautySalonBooking.Maui.Features.Home.ViewModels;

namespace BeautySalonBooking.Maui.Features.Home.Views;

public partial class HomeView : ContentView
{
    private readonly HomeViewModel _viewModel;
    private readonly IDialogService _dialogService;
    private readonly IUserContext _userContext;

    public HomeView(
        HomeViewModel viewModel,
        IDialogService dialogService,
        IUserContext userContext)
    {
        InitializeComponent();

        _viewModel = viewModel;
        _userContext = userContext;
        _dialogService = dialogService;

        BindingContext = viewModel;

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, EventArgs e)
    {
        Loaded -= OnLoaded;

        //await _dialogService.ShowInfoAsync(
        //    "UserContext",
        //    $"Id: {_userContext.UserId}\n" +
        //    $"Name: {_userContext.FullName}\n" +
        //    $"Phone: {_userContext.PhoneNumber}\n" +
        //    $"Role: {string.Join(", ", _userContext.Roles)}");

        await _viewModel.LoadCategoriesAsync();
    }
}