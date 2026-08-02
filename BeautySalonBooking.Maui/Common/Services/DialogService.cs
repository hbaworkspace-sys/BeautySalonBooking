using BeautySalonBooking.Maui.Common.Interfaces;

namespace BeautySalonBooking.Maui.Common.Services;

public sealed class DialogService : IDialogService
{
    public Task ShowErrorAsync(string message)
    {
        return Shell.Current.DisplayAlert(
            "خطا",
            message,
            "باشه");
    }

    public Task ShowSuccessAsync(string message)
    {
        return Shell.Current.DisplayAlert(
            "موفق",
            message,
            "باشه");
    }

    public Task ShowInfoAsync(
        string title,
        string message)
    {
        return Shell.Current.DisplayAlert(
            title,
            message,
            "باشه");
    }

    public Task<bool> ShowConfirmationAsync(
        string title,
        string message,
        string accept = "بله",
        string cancel = "خیر")
    {
        return Shell.Current.DisplayAlert(
            title,
            message,
            accept,
            cancel);
    }
}