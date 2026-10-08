using BeautySalonBooking.Maui.Features.Profile.ViewModels;

namespace BeautySalonBooking.Maui.Features.Profile.Views;

public partial class ProfileView : ContentView
{
    public ProfileView(ProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, EventArgs e)
    {
        Loaded -= OnLoaded;
        if (BindingContext is ProfileViewModel viewModel)
            await viewModel.InitializeAsync();

        await MyProgressBar.ProgressTo(
            0.75,
            800,
            Easing.CubicOut);
    }
}
