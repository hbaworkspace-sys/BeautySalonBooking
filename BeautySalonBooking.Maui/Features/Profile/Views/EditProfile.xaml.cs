using BeautySalonBooking.Maui.Features.Profile.ViewModels;

namespace BeautySalonBooking.Maui.Features.Profile.Views;

public partial class EditProfile : ContentPage
{
    public EditProfile(EditProfileViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}