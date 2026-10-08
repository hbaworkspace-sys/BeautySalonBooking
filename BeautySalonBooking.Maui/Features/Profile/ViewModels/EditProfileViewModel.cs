using BeautySalonBooking.Maui.Common.Interfaces;
using BeautySalonBooking.Maui.Features.Popups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalonBooking.Maui.Features.Profile.ViewModels;

public partial class EditProfileViewModel :
    ObservableObject
{
    private readonly INavigationService _navigationService;
    public EditProfileViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [ObservableProperty]
    private ImageSource? profileImage;

    [RelayCommand]
    private async Task ChangeImageAsync()
    {
        var result = await Shell.Current.ShowPopupAsync(
                new ProfileImagePopup());

        if (result is not FileResult photo)
            return;

        ProfileImage = ImageSource.FromFile(photo.FullPath);
        // AvatarImage = await Shell.Current.ShowPopupAsync(new ProfileImagePopup());
        //try
        //{
        //    var photo = await MediaPicker.Default.PickPhotoAsync();
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
    }

    [RelayCommand]
    private Task GoBackAsync()
    {
        return _navigationService.GoBackAsync();
    }
}
