namespace BeautySalonBooking.Maui.Common.Interfaces;

public interface IDialogService
{
    Task ShowErrorAsync(string message);

    Task ShowSuccessAsync(string message);

    Task ShowInfoAsync(string title, string message);

    Task<bool> ShowConfirmationAsync(
        string title,
        string message,
        string accept = "بله",
        string cancel = "خیر");
}