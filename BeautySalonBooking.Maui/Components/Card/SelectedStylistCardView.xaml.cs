namespace BeautySalonBooking.Maui.Components.Card;

public partial class SelectedStylistCardView : ContentView
{
    public SelectedStylistCardView()
    {
        InitializeComponent();

        UpdateImage();
        UpdateStylistName();
        UpdateStylistTitle();
        UpdateServiceName();
        UpdateDuration();
        UpdatePrice();
    }


    #region Image

    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(
            nameof(ImageSource),
            typeof(ImageSource),
            typeof(SelectedStylistCardView),
            default(ImageSource),
            propertyChanged: OnImageSourceChanged);

    private static void OnImageSourceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SelectedStylistCardView control)
            control.UpdateImage();
    }

    private void UpdateImage()
    {
        StylistImageView.ImageSource = ImageSource;
    }

    #endregion


    #region Stylist Name

    public string? StylistName
    {
        get => (string?)GetValue(StylistNameProperty);
        set => SetValue(StylistNameProperty, value);
    }

    public static readonly BindableProperty StylistNameProperty =
        BindableProperty.Create(
            nameof(StylistName),
            typeof(string),
            typeof(SelectedStylistCardView),
            default(string),
            propertyChanged: OnStylistNameChanged);

    private static void OnStylistNameChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SelectedStylistCardView control)
            control.UpdateStylistName();
    }

    private void UpdateStylistName()
    {
        StylistNameLabel.Text = StylistName;
    }

    #endregion


    #region Stylist Title

    public string? StylistTitle
    {
        get => (string?)GetValue(StylistTitleProperty);
        set => SetValue(StylistTitleProperty, value);
    }

    public static readonly BindableProperty StylistTitleProperty =
        BindableProperty.Create(
            nameof(StylistTitle),
            typeof(string),
            typeof(SelectedStylistCardView),
            default(string),
            propertyChanged: OnStylistTitleChanged);

    private static void OnStylistTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SelectedStylistCardView control)
            control.UpdateStylistTitle();
    }

    private void UpdateStylistTitle()
    {
        StylistTitleLabel.Text = StylistTitle;
    }

    #endregion


    #region Service Name

    public string? ServiceName
    {
        get => (string?)GetValue(ServiceNameProperty);
        set => SetValue(ServiceNameProperty, value);
    }

    public static readonly BindableProperty ServiceNameProperty =
        BindableProperty.Create(
            nameof(ServiceName),
            typeof(string),
            typeof(SelectedStylistCardView),
            default(string),
            propertyChanged: OnServiceNameChanged);

    private static void OnServiceNameChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SelectedStylistCardView control)
            control.UpdateServiceName();
    }

    private void UpdateServiceName()
    {
        ServiceNameLabel.Text = ServiceName;
    }

    #endregion


    #region Duration

    public string? Duration
    {
        get => (string?)GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }

    public static readonly BindableProperty DurationProperty =
        BindableProperty.Create(
            nameof(Duration),
            typeof(string),
            typeof(SelectedStylistCardView),
            default(string),
            propertyChanged: OnDurationChanged);

    private static void OnDurationChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SelectedStylistCardView control)
            control.UpdateDuration();
    }

    private void UpdateDuration()
    {
        DurationLabel.Text = Duration;
    }

    #endregion


    #region Price

    public string? Price
    {
        get => (string?)GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    public static readonly BindableProperty PriceProperty =
        BindableProperty.Create(
            nameof(Price),
            typeof(string),
            typeof(SelectedStylistCardView),
            default(string),
            propertyChanged: OnPriceChanged);

    private static void OnPriceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SelectedStylistCardView control)
            control.UpdatePrice();
    }

    private void UpdatePrice()
    {
        PriceLabel.Text = Price;
    }

    #endregion
}