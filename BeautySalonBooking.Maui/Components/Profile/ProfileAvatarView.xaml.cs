namespace BeautySalonBooking.Maui.Components.Profile;

public partial class ProfileAvatarView : ContentView
{
    public ProfileAvatarView()
    {
        InitializeComponent();
        UpdateInitials();
    }

    public string? Initials
    {
        get => (string?)GetValue(InitialsProperty);
        set => SetValue(InitialsProperty, value);
    }

    public static readonly BindableProperty InitialsProperty =
        BindableProperty.Create(
            nameof(Initials),
            typeof(string),
            typeof(ProfileAvatarView),
            string.Empty,
            propertyChanged: OnInitialsChanged);

    public Color AccentColor
    {
        get => (Color)GetValue(AccentColorProperty);
        set => SetValue(AccentColorProperty, value);
    }

    public static readonly BindableProperty AccentColorProperty =
        BindableProperty.Create(
            nameof(AccentColor),
            typeof(Color),
            typeof(ProfileAvatarView),
            Color.FromArgb("#ED5F91"),
            propertyChanged: OnAppearanceChanged);

    public string? AvatarKey
    {
        get => (string?)GetValue(AvatarKeyProperty);
        set => SetValue(AvatarKeyProperty, value);
    }

    public static readonly BindableProperty AvatarKeyProperty =
        BindableProperty.Create(
            nameof(AvatarKey),
            typeof(string),
            typeof(ProfileAvatarView),
            "rose",
            propertyChanged: OnAvatarKeyChanged);

    public ImageSource? Image
    {
        get => (ImageSource?)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }
    public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(
                nameof(Image),
                typeof(ImageSource),
                typeof(ProfileAvatarView),
                default(ImageSource?),
                propertyChanged: OnImageChanged);


    private static void OnInitialsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ProfileAvatarView control)
            control.UpdateInitials();
    }

    private static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ProfileAvatarView control)
        {
            control.UpdateImage();
        }
    }
    private static void OnAppearanceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ProfileAvatarView control)
            control.UpdateAppearance();
    }

    private static void OnAvatarKeyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ProfileAvatarView control)
            control.UpdateAvatar();
    }

    private void UpdateInitials()
    {
        InitialsLabel.Text = string.IsNullOrWhiteSpace(Initials) ? "؟" : Initials.Trim();
        AvatarBorder.HeightRequest = HeightRequest;
        AvatarBorder.WidthRequest = WidthRequest;
    }

    private void UpdateAppearance()
    {
        AvatarBorder.Stroke = AccentColor;
        InitialsLabel.TextColor = AccentColor;
    }

    private void UpdateAvatar()
    {
        AccentColor = AvatarKey switch
        {
            "lilac" => Color.FromArgb("#8B6FC8"),
            "peach" => Color.FromArgb("#D97857"),
            _ => Color.FromArgb("#ED5F91")
        };
    }
    private void UpdateImage()
    {
        AvatarImage.Source = Image;
    }
}
