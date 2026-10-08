using BeautySalonBooking.Maui.Common.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Profile.ViewModels;

public partial class DeleteAccountViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;

    public DeleteAccountViewModel(
        INavigationService navigationService,
        IDialogService dialogService)
    {
        _navigationService = navigationService;
        _dialogService = dialogService;
    }
    //[ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(CanDeleteAccount))]
    //private string deleteConfirmationText = string.Empty;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    //public bool HasError =>
    //    !string.IsNullOrWhiteSpace(ErrorMessage);

    //public bool CanDeleteAccount =>
    //    DeleteConfirmationText.Trim() == "حذف";

    //[RelayCommand]
    //private async Task DeleteAccountAsync()
    //{
    //    ErrorMessage = string.Empty;

    //    if (!CanDeleteAccount)
    //    {
    //        ErrorMessage = "لطفاً عبارت «حذف» را وارد کنید.";
    //        OnPropertyChanged(nameof(HasError));
    //        return;
    //    }

    //    await _dialogService.ShowInfoAsync(
    //        "حذف حساب در مرحله بعد به سرویس واقعی متصل می‌شود.");
    //}

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}