using CommunityToolkit.Maui.Views;
using System.Windows.Input;
namespace BeautySalonBooking.Maui.Features.Popups;

public partial class ProfileImagePopup : Popup
{
    public ICommand CameraCommand { get; set; }
    public ICommand GalleryCommand { get; set; }
    public ICommand FileCommand { get; set; }

    public ProfileImagePopup()
    {
        InitializeComponent();

        //CameraCommand = new Command(async()=> await OnCameraClicked());
        //GalleryCommand = new Command(async()=> await OnGalleryClicked());
        //FileCommand = new Command(async() => await OnFileClicked());


        CameraCommand = new Command(() =>
        {
            System.Diagnostics.Debug.WriteLine("CAMERA COMMAND");
        });

        GalleryCommand = new Command(() =>
        {
            System.Diagnostics.Debug.WriteLine("GALLERY COMMAND");
        });

        FileCommand = new Command(() =>
        {
            System.Diagnostics.Debug.WriteLine("FILE COMMAND");
        });


        BindingContext = this;
    }

    private async Task OnGalleryClicked()
    {
        var photo = await MediaPicker.Default.PickPhotoAsync();

        if (photo is null)
            return;

        await CloseAsync();
    }

    private async Task OnCameraClicked()
    {
        var photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo is null)
            return;

        await CloseAsync();
    }

    private async Task OnFileClicked()
    {
        var photo = await FilePicker.Default.PickAsync();

        if (photo is null)
            return;

        await CloseAsync();
    }

    private async void BtnLibrary_Clicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.Default.PickPhotoAsync();

        if (photo is null)
            return;

        await CloseAsync(photo);
    }

    private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo is null)
            return;

        await CloseAsync();
    }

    private async void BtnFile_Clicked(object sender, EventArgs e)
    {
        var photo = await FilePicker.Default.PickAsync();

        if (photo is null)
            return;

        await CloseAsync();
    }
}