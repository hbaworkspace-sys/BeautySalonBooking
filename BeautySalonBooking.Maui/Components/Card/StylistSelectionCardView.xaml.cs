using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Card;

public partial class StylistSelectionCardView : ContentView
{
    public StylistSelectionCardView()
    {
        InitializeComponent();

        UpdateImage();
        UpdateStylistName();
        UpdateStylistTitle();
        UpdateRating();
        UpdateDuration();
        UpdatePrice();
        UpdateFirstAvailableTime();
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
            typeof(StylistSelectionCardView),
            default(ImageSource),
            propertyChanged: OnImageSourceChanged);

    private static void OnImageSourceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (StylistSelectionCardView)bindable;

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
            typeof(StylistSelectionCardView),
            default(string),
            propertyChanged: OnStylistNameChanged);

    private static void OnStylistNameChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (StylistSelectionCardView)bindable;

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
            typeof(StylistSelectionCardView),
            default(string),
            propertyChanged: OnStylistTitleChanged);
    private static void OnStylistTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (StylistSelectionCardView)bindable;

        control.UpdateStylistTitle();
    }
    private void UpdateStylistTitle()
    {
        StylistTitleLabel.Text = StylistTitle;
    }
    #endregion

    #region Rating

    public double Rating
    {
        get => (double)GetValue(RatingProperty);
        set => SetValue(RatingProperty, value);
    }

    public static readonly BindableProperty RatingProperty =
        BindableProperty.Create(
            nameof(Rating),
            typeof(double),
            typeof(StylistSelectionCardView),
            0.0,
            propertyChanged: OnRatingChanged);


    private static void OnRatingChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (StylistSelectionCardView)bindable;

        control.UpdateRating();
    }


    private void UpdateRating()
    {
        RatingLabel.Text = Rating.ToString("0.0");

        var roundedRating = (int)Math.Round(
            Rating,
            MidpointRounding.AwayFromZero);

        roundedRating = Math.Clamp(roundedRating, 0, 5);

        var stars = RatingStarsLayout.Children
            .OfType<Label>()
            .ToList();

        for (var i = 0; i < stars.Count; i++)
        {
            stars[i].TextColor =
                i < roundedRating
                    ? Color.FromArgb("#F05C80")
                    : Color.FromArgb("#D9D3D6");
        }
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
            typeof(StylistSelectionCardView),
            default(string),
            propertyChanged: OnDurationChanged);
    private static void OnDurationChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (StylistSelectionCardView)bindable;

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
            typeof(StylistSelectionCardView),
            default(string),
            propertyChanged: OnPriceChanged);

    private static void OnPriceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (StylistSelectionCardView)bindable;

        control.UpdatePrice();
    }

    private void UpdatePrice()
    {
        PriceLabel.Text = Price;
    }
    #endregion

    #region First Available Time
    public string? FirstAvailableTime
    {
        get => (string?)GetValue(FirstAvailableTimeProperty);
        set => SetValue(FirstAvailableTimeProperty, value);
    }
    public static readonly BindableProperty FirstAvailableTimeProperty =
        BindableProperty.Create(
            nameof(FirstAvailableTime),
            typeof(string),
            typeof(StylistSelectionCardView),
            default(string),
            propertyChanged: OnFirstAvailableTimeChanged);

    private static void OnFirstAvailableTimeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (StylistSelectionCardView)bindable;

        control.UpdateFirstAvailableTime();
    }
    private void UpdateFirstAvailableTime()
    {
        FirstAvailableTimeLabel.Text = FirstAvailableTime;
    }
    #endregion


    public static readonly BindableProperty CommandProperty =
    BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(StylistSelectionCardView));

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(StylistSelectionCardView));

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
}