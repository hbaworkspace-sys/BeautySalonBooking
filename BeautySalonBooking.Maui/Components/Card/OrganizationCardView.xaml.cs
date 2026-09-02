using BeautySalonBooking.Maui.Components.Shared.Enums;
using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Card;

public partial class OrganizationCardView : ContentView
{
    public OrganizationCardView()
    {
        InitializeComponent();

        UpdateCardLayout();
        UpdateCardAppearance();
        UpdateBranchName();
        UpdateSalonType();
        UpdateRating();
        UpdateBeauticianCount();
        UpdateAvailableTimes();
        UpdateImage();
        UpdateBadge();
    }


    #region Card Layout
    public Thickness CardPadding
    {
        get => (Thickness)GetValue(CardPaddingProperty);
        set => SetValue(CardPaddingProperty, value);
    }
    public static readonly BindableProperty CardPaddingProperty =
        BindableProperty.Create(
            nameof(CardPadding),
            typeof(Thickness),
            typeof(OrganizationCardView),
            new Thickness(0, 0),
            propertyChanged: OnCardLayoutChanged);


    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(OrganizationCardView),
            14d,
            propertyChanged: OnCardLayoutChanged);

    private static void OnCardLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateCardLayout();
    }

    private void UpdateCardLayout()
    {
        ContainerBorder.Padding = CardPadding;
        CardShape.CornerRadius = new CornerRadius(CornerRadius);
    }
    #endregion


    #region Card Appearance

    public Brush? FillBrush
    {
        get => (Brush?)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }

    public static readonly BindableProperty FillBrushProperty =
        BindableProperty.Create(
            nameof(FillBrush),
            typeof(Brush),
            typeof(OrganizationCardView),
            new SolidColorBrush(Colors.White),
            propertyChanged: OnCardAppearanceChanged);


    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(OrganizationCardView),
            new SolidColorBrush(Colors.Pink),
            propertyChanged: OnCardAppearanceChanged);


    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(OrganizationCardView),
            1d,
            propertyChanged: OnCardAppearanceChanged);


    private static void OnCardAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateCardAppearance();
    }

    private void UpdateCardAppearance()
    {
        ContainerBorder.Background = FillBrush;
        ContainerBorder.Stroke = BorderBrush;
        ContainerBorder.StrokeThickness = BorderWidth;
    }

    #endregion


    #region Organization

    public string? OrganizationName
    {
        get => (string?)GetValue(OrganizationNameProperty);
        set => SetValue(OrganizationNameProperty, value);
    }

    public static readonly BindableProperty OrganizationNameProperty =
        BindableProperty.Create(
            nameof(OrganizationName),
            typeof(string),
            typeof(OrganizationCardView),
            default(string),
            propertyChanged: OnBranchNameChanged);


    private static void OnBranchNameChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateBranchName();
    }


    private void UpdateBranchName()
    {
        BranchNameLabel.Text = OrganizationName;
    }

    #endregion


    #region Branch Title

    public string? BranchTitle
    {
        get => (string?)GetValue(BranchTitleProperty);
        set => SetValue(BranchTitleProperty, value);
    }

    public static readonly BindableProperty BranchTitleProperty =
        BindableProperty.Create(
            nameof(BranchTitle),
            typeof(string),
            typeof(OrganizationCardView),
            default(string),
            propertyChanged: OnSalonTypeChanged);


    private static void OnSalonTypeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateSalonType();
    }


    private void UpdateSalonType()
    {
        BranchTitleLabel.Text = BranchTitle;
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
            typeof(OrganizationCardView),
            0d,
            propertyChanged: OnRatingChanged);


    private static void OnRatingChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateRating();
    }


    private void UpdateRating()
    {
        RatingLabel.Text = Rating.ToString("0.0");
    }

    #endregion


    #region Beauticians

    public int BeauticianCount
    {
        get => (int)GetValue(BeauticianCountProperty);
        set => SetValue(BeauticianCountProperty, value);
    }

    public static readonly BindableProperty BeauticianCountProperty =
        BindableProperty.Create(
            nameof(BeauticianCount),
            typeof(int),
            typeof(OrganizationCardView),
            0,
            propertyChanged: OnBeauticianCountChanged);


    private static void OnBeauticianCountChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateBeauticianCount();
    }


    private void UpdateBeauticianCount()
    {
        BeauticianCountLabel.Text = $"{BeauticianCount} آرایشگر";
    }

    #endregion


    #region Available Times

    public int AvailableTimes
    {
        get => (int)GetValue(AvailableTimesProperty);
        set => SetValue(AvailableTimesProperty, value);
    }

    public static readonly BindableProperty AvailableTimesProperty =
        BindableProperty.Create(
            nameof(AvailableTimes),
            typeof(int),
            typeof(OrganizationCardView),
            0,
            propertyChanged: OnAvailableTimesChanged);


    private static void OnAvailableTimesChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateAvailableTimes();
    }


    private void UpdateAvailableTimes()
    {
        AvailableTimesLabel.Text =
            $"امروز {AvailableTimes} تایم آزاد";
    }

    #endregion


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
            typeof(OrganizationCardView),
            default(ImageSource),
            propertyChanged: OnImageChanged);


    public double ImageSize
    {
        get => (double)GetValue(ImageSizeProperty);
        set => SetValue(ImageSizeProperty, value);
    }

    public static readonly BindableProperty ImageSizeProperty =
        BindableProperty.Create(
            nameof(ImageSize),
            typeof(double),
            typeof(OrganizationCardView),
            150d,
            propertyChanged: OnImageChanged);


    public double ImageCornerRadius
    {
        get => (double)GetValue(ImageCornerRadiusProperty);
        set => SetValue(ImageCornerRadiusProperty, value);
    }

    public static readonly BindableProperty ImageCornerRadiusProperty =
        BindableProperty.Create(
            nameof(ImageCornerRadius),
            typeof(double),
            typeof(OrganizationCardView),
            12d,
            propertyChanged: OnImageChanged);


    private static void OnImageChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateImage();
    }


    private void UpdateImage()
    {
        SalonImageView.ImageSource = ImageSource;

        SalonImageView.WidthRequest = ImageSize;
        SalonImageView.HeightRequest = ImageSize;

        SalonImageView.CornerRadius = ImageCornerRadius;
    }

    #endregion



    #region Badge

    public BadgeType BadgeType
    {
        get => (BadgeType)GetValue(BadgeTypeProperty);
        set => SetValue(BadgeTypeProperty, value);
    }

    public static readonly BindableProperty BadgeTypeProperty =
        BindableProperty.Create(
            nameof(BadgeType),
            typeof(BadgeType),
            typeof(OrganizationCardView),
            BadgeType.None,
            propertyChanged: OnBadgeChanged);


    public double BadgeFontSize
    {
        get => (double)GetValue(BadgeFontSizeProperty);
        set => SetValue(BadgeFontSizeProperty, value);
    }

    public static readonly BindableProperty BadgeFontSizeProperty =
        BindableProperty.Create(
            nameof(BadgeFontSize),
            typeof(double),
            typeof(OrganizationCardView),
            9d,
            propertyChanged: OnBadgeChanged);


    public FontAttributes BadgeFontAttributes
    {
        get => (FontAttributes)GetValue(BadgeFontAttributesProperty);
        set => SetValue(BadgeFontAttributesProperty, value);
    }

    public static readonly BindableProperty BadgeFontAttributesProperty =
        BindableProperty.Create(
            nameof(BadgeFontAttributes),
            typeof(FontAttributes),
            typeof(OrganizationCardView),
            FontAttributes.Bold,
            propertyChanged: OnBadgeChanged);


    public double BadgeWidth
    {
        get => (double)GetValue(BadgeWidthProperty);
        set => SetValue(BadgeWidthProperty, value);
    }

    public static readonly BindableProperty BadgeWidthProperty =
        BindableProperty.Create(
            nameof(BadgeWidth),
            typeof(double),
            typeof(OrganizationCardView),
            70d,
            propertyChanged: OnBadgeChanged);


    public double BadgeHeight
    {
        get => (double)GetValue(BadgeHeightProperty);
        set => SetValue(BadgeHeightProperty, value);
    }

    public static readonly BindableProperty BadgeHeightProperty =
        BindableProperty.Create(
            nameof(BadgeHeight),
            typeof(double),
            typeof(OrganizationCardView),
            30d,
            propertyChanged: OnBadgeChanged);


    public CornerRadius BadgeCornerRadius
    {
        get => (CornerRadius)GetValue(BadgeCornerRadiusProperty);
        set => SetValue(BadgeCornerRadiusProperty, value);
    }

    public static readonly BindableProperty BadgeCornerRadiusProperty =
        BindableProperty.Create(
            nameof(BadgeCornerRadius),
            typeof(CornerRadius),
            typeof(OrganizationCardView),
            new CornerRadius(10),
            propertyChanged: OnBadgeChanged);


    public Thickness BadgeMargin
    {
        get => (Thickness)GetValue(BadgeMarginProperty);
        set => SetValue(BadgeMarginProperty, value);
    }

    public static readonly BindableProperty BadgeMarginProperty =
        BindableProperty.Create(
            nameof(BadgeMargin),
            typeof(Thickness),
            typeof(OrganizationCardView),
            new Thickness(0, 5, 5, 0),
            propertyChanged: OnBadgeChanged);


    private static void OnBadgeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is OrganizationCardView control)
            control.UpdateBadge();
    }

    private void UpdateBadge()
    {
        BadgeView.IsVisible = BadgeType != BadgeType.None;

        BadgeView.BadgeType = BadgeType;

        BadgeView.FontSize = BadgeFontSize;
        BadgeView.FontAttributes = BadgeFontAttributes;
        BadgeView.WidthRequest = BadgeWidth;
        BadgeView.HeightRequest = BadgeHeight;
        BadgeView.CornerRadius = BadgeCornerRadius;
        BadgeView.Margin = BadgeMargin;
    }

    #endregion

    public static readonly BindableProperty CommandProperty =
BindableProperty.Create(
    nameof(Command),
    typeof(ICommand),
    typeof(OrganizationCardView));

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(OrganizationCardView));

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }



}