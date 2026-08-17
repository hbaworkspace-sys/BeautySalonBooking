using System.Windows.Input;
using Icon = BeautySalonBooking.Maui.Resources;
namespace BeautySalonBooking.Maui.Components.Card;

public partial class ShowcaseCardView : ContentView
{
    public ShowcaseCardView()
    {
        InitializeComponent();

        UpdateCardLayout();
        UpdateCardAppearance();
        UpdateCardShadow();

        UpdateFavorite();
        UpdateBookmark();

        UpdateImage();
        UpdateOverlay();
        UpdateActionLayout();
        UpdateInformationLayout();
        UpdateContent();

        UpdateBadge();
        UpdateCardBackgroundImage();
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
            typeof(ShowcaseCardView),
            new Thickness(0),
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
            typeof(ShowcaseCardView),
            10d,
            propertyChanged: OnCardLayoutChanged);


    private static void OnCardLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateCardLayout();
    }

    private void UpdateCardLayout()
    {
        ContainerBorder.Padding = CardPadding;
        CardShape.CornerRadius = new CornerRadius(CornerRadius);
    }

    #endregion

    #region Information Layout

    public TextAlignment InformationTextAlignment
    {
        get => (TextAlignment)GetValue(InformationTextAlignmentProperty);
        set => SetValue(InformationTextAlignmentProperty, value);
    }

    public static readonly BindableProperty InformationTextAlignmentProperty =
        BindableProperty.Create(
            nameof(InformationTextAlignment),
            typeof(TextAlignment),
            typeof(ShowcaseCardView),
            TextAlignment.Start,
            propertyChanged: OnInformationLayoutChanged);

    private static void OnInformationLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateInformationLayout();
    }

    private void UpdateInformationLayout()
    {
        TitleLabel.HorizontalTextAlignment = InformationTextAlignment;
        SubtitleLable.HorizontalTextAlignment = InformationTextAlignment;
        RatingLabel.HorizontalTextAlignment = InformationTextAlignment;
    }

    #endregion

    #region Card Appearance
    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(ShowcaseCardView),
            null,
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
            typeof(ShowcaseCardView),
            1.5d,
            propertyChanged: OnCardAppearanceChanged);


    public Brush? FillBrush
    {
        get => (Brush?)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }

    public static readonly BindableProperty FillBrushProperty =
        BindableProperty.Create(
            nameof(FillBrush),
            typeof(Brush),
            typeof(ShowcaseCardView),
            null,
            propertyChanged: OnCardAppearanceChanged);


    private static void OnCardAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateCardAppearance();
    }


    private void UpdateCardAppearance()
    {
        ContainerBorder.Stroke = BorderBrush;
        ContainerBorder.StrokeThickness = BorderWidth;
        ContainerBorder.Background = FillBrush;
    }

    #endregion

    #region Card Shadow
    public Shadow? CardShadow
    {
        get => (Shadow?)GetValue(CardShadowProperty);
        set => SetValue(CardShadowProperty, value);
    }

    public static readonly BindableProperty CardShadowProperty =
        BindableProperty.Create(
            nameof(CardShadow),
            typeof(Shadow),
            typeof(ShowcaseCardView),
            null,
            propertyChanged: OnCardShadowChanged);


    private static void OnCardShadowChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateCardShadow();
    }


    private void UpdateCardShadow()
    {
        ContainerBorder.Shadow = CardShadow;
    }

    #endregion

    #region Card Command

    public ICommand? TapCommand
    {
        get => (ICommand?)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(
            nameof(TapCommand),
            typeof(ICommand),
            typeof(ShowcaseCardView),
            default(ICommand));


    public object? TapCommandParameter
    {
        get => GetValue(TapCommandParameterProperty);
        set => SetValue(TapCommandParameterProperty, value);
    }

    public static readonly BindableProperty TapCommandParameterProperty =
        BindableProperty.Create(
            nameof(TapCommandParameter),
            typeof(object),
            typeof(ShowcaseCardView),
            default(object));

    #endregion

    #region Favorite

    public string? FavoriteIcon
    {
        get => (string?)GetValue(FavoriteIconProperty);
        set => SetValue(FavoriteIconProperty, value);
    }

    public static readonly BindableProperty FavoriteIconProperty =
        BindableProperty.Create(
            nameof(FavoriteIcon),
            typeof(string),
            typeof(ShowcaseCardView),
           Icon.IconFont.FavoriteBorder,
            propertyChanged: OnFavoriteChanged);


    public string? FavoriteSelectedIcon
    {
        get => (string?)GetValue(FavoriteSelectedIconProperty);
        set => SetValue(FavoriteSelectedIconProperty, value);
    }

    public static readonly BindableProperty FavoriteSelectedIconProperty =
        BindableProperty.Create(
            nameof(FavoriteSelectedIcon),
            typeof(string),
            typeof(ShowcaseCardView),
            Icon.IconFont.Favorite,
            propertyChanged: OnFavoriteChanged);


    public bool FavoriteIsSelected
    {
        get => (bool)GetValue(FavoriteIsSelectedProperty);
        set => SetValue(FavoriteIsSelectedProperty, value);
    }

    public static readonly BindableProperty FavoriteIsSelectedProperty =
        BindableProperty.Create(
            nameof(FavoriteIsSelected),
            typeof(bool),
            typeof(ShowcaseCardView),
            false,
            propertyChanged: OnFavoriteChanged);


    public double FavoriteIconSize
    {
        get => (double)GetValue(FavoriteIconSizeProperty);
        set => SetValue(FavoriteIconSizeProperty, value);
    }

    public static readonly BindableProperty FavoriteIconSizeProperty =
        BindableProperty.Create(
            nameof(FavoriteIconSize),
            typeof(double),
            typeof(ShowcaseCardView),
            23d,
            propertyChanged: OnFavoriteChanged);


    public Color FavoriteIconColor
    {
        get => (Color)GetValue(FavoriteIconColorProperty);
        set => SetValue(FavoriteIconColorProperty, value);
    }

    public static readonly BindableProperty FavoriteIconColorProperty =
        BindableProperty.Create(
            nameof(FavoriteIconColor),
            typeof(Color),
            typeof(ShowcaseCardView),
            Colors.White,
            propertyChanged: OnFavoriteChanged);


    public Color FavoriteSelectedIconColor
    {
        get => (Color)GetValue(FavoriteSelectedIconColorProperty);
        set => SetValue(FavoriteSelectedIconColorProperty, value);
    }

    public static readonly BindableProperty FavoriteSelectedIconColorProperty =
        BindableProperty.Create(
            nameof(FavoriteSelectedIconColor),
            typeof(Color),
            typeof(ShowcaseCardView),
            Colors.Black,
            propertyChanged: OnFavoriteChanged);


    public double FavoriteTouchSize
    {
        get => (double)GetValue(FavoriteTouchSizeProperty);
        set => SetValue(FavoriteTouchSizeProperty, value);
    }

    public static readonly BindableProperty FavoriteTouchSizeProperty =
        BindableProperty.Create(
            nameof(FavoriteTouchSize),
            typeof(double),
            typeof(ShowcaseCardView),
            25d,
            propertyChanged: OnFavoriteChanged);


    public ICommand? FavoriteCommand
    {
        get => (ICommand?)GetValue(FavoriteCommandProperty);
        set => SetValue(FavoriteCommandProperty, value);
    }

    public static readonly BindableProperty FavoriteCommandProperty =
        BindableProperty.Create(
            nameof(FavoriteCommand),
            typeof(ICommand),
            typeof(ShowcaseCardView),
            default(ICommand),
            propertyChanged: OnFavoriteChanged);


    public object? FavoriteCommandParameter
    {
        get => GetValue(FavoriteCommandParameterProperty);
        set => SetValue(FavoriteCommandParameterProperty, value);
    }

    public static readonly BindableProperty FavoriteCommandParameterProperty =
        BindableProperty.Create(
            nameof(FavoriteCommandParameter),
            typeof(object),
            typeof(ShowcaseCardView),
            default(object),
            propertyChanged: OnFavoriteChanged);


    private static void OnFavoriteChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateFavorite();
    }


    private void UpdateFavorite()
    {
        FavoriteAction.Icon = FavoriteIcon;
        FavoriteAction.SelectedIcon = FavoriteSelectedIcon;
        FavoriteAction.IsSelected = FavoriteIsSelected;

        FavoriteAction.IconSize = FavoriteIconSize;
        FavoriteAction.IconColor = FavoriteIconColor;
        FavoriteAction.SelectedIconColor = FavoriteSelectedIconColor;

        FavoriteAction.TouchSize = FavoriteTouchSize;

        FavoriteAction.Command = FavoriteCommand;
        FavoriteAction.CommandParameter = FavoriteCommandParameter;
    }

    #endregion

    #region Bookmark
    public string? BookmarkIcon
    {
        get => (string?)GetValue(BookmarkIconProperty);
        set => SetValue(BookmarkIconProperty, value);
    }

    public static readonly BindableProperty BookmarkIconProperty =
        BindableProperty.Create(
            nameof(BookmarkIcon),
            typeof(string),
            typeof(ShowcaseCardView),
            Icon.IconFont.BookmarkBorder,
            propertyChanged: OnBookmarkChanged);


    public string? BookmarkSelectedIcon
    {
        get => (string?)GetValue(BookmarkSelectedIconProperty);
        set => SetValue(BookmarkSelectedIconProperty, value);
    }

    public static readonly BindableProperty BookmarkSelectedIconProperty =
        BindableProperty.Create(
            nameof(BookmarkSelectedIcon),
            typeof(string),
            typeof(ShowcaseCardView),
            Icon.IconFont.Bookmark,
            propertyChanged: OnBookmarkChanged);


    public bool BookmarkIsSelected
    {
        get => (bool)GetValue(BookmarkIsSelectedProperty);
        set => SetValue(BookmarkIsSelectedProperty, value);
    }

    public static readonly BindableProperty BookmarkIsSelectedProperty =
        BindableProperty.Create(
            nameof(BookmarkIsSelected),
            typeof(bool),
            typeof(ShowcaseCardView),
            false,
            propertyChanged: OnBookmarkChanged);


    public double BookmarkIconSize
    {
        get => (double)GetValue(BookmarkIconSizeProperty);
        set => SetValue(BookmarkIconSizeProperty, value);
    }

    public static readonly BindableProperty BookmarkIconSizeProperty =
        BindableProperty.Create(
            nameof(BookmarkIconSize),
            typeof(double),
            typeof(ShowcaseCardView),
            27d,
            propertyChanged: OnBookmarkChanged);


    public Color BookmarkIconColor
    {
        get => (Color)GetValue(BookmarkIconColorProperty);
        set => SetValue(BookmarkIconColorProperty, value);
    }

    public static readonly BindableProperty BookmarkIconColorProperty =
        BindableProperty.Create(
            nameof(BookmarkIconColor),
            typeof(Color),
            typeof(ShowcaseCardView),
            Colors.Black,
            propertyChanged: OnBookmarkChanged);


    public Color BookmarkSelectedIconColor
    {
        get => (Color)GetValue(BookmarkSelectedIconColorProperty);
        set => SetValue(BookmarkSelectedIconColorProperty, value);
    }

    public static readonly BindableProperty BookmarkSelectedIconColorProperty =
        BindableProperty.Create(
            nameof(BookmarkSelectedIconColor),
            typeof(Color),
            typeof(ShowcaseCardView),
            Colors.Black,
            propertyChanged: OnBookmarkChanged);


    public double BookmarkTouchSize
    {
        get => (double)GetValue(BookmarkTouchSizeProperty);
        set => SetValue(BookmarkTouchSizeProperty, value);
    }

    public static readonly BindableProperty BookmarkTouchSizeProperty =
        BindableProperty.Create(
            nameof(BookmarkTouchSize),
            typeof(double),
            typeof(ShowcaseCardView),
            28d,
            propertyChanged: OnBookmarkChanged);


    public ICommand? BookmarkCommand
    {
        get => (ICommand?)GetValue(BookmarkCommandProperty);
        set => SetValue(BookmarkCommandProperty, value);
    }

    public static readonly BindableProperty BookmarkCommandProperty =
        BindableProperty.Create(
            nameof(BookmarkCommand),
            typeof(ICommand),
            typeof(ShowcaseCardView),
            default(ICommand),
            propertyChanged: OnBookmarkChanged);


    public object? BookmarkCommandParameter
    {
        get => GetValue(BookmarkCommandParameterProperty);
        set => SetValue(BookmarkCommandParameterProperty, value);
    }

    public static readonly BindableProperty BookmarkCommandParameterProperty =
        BindableProperty.Create(
            nameof(BookmarkCommandParameter),
            typeof(object),
            typeof(ShowcaseCardView),
            default(object),
            propertyChanged: OnBookmarkChanged);


    private static void OnBookmarkChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateBookmark();
    }

    private void UpdateBookmark()
    {
        BookmarkAction.Icon = BookmarkIcon;
        BookmarkAction.SelectedIcon = BookmarkSelectedIcon;
        BookmarkAction.IsSelected = BookmarkIsSelected;

        BookmarkAction.IconSize = BookmarkIconSize;
        BookmarkAction.IconColor = BookmarkIconColor;
        BookmarkAction.SelectedIconColor = BookmarkSelectedIconColor;

        BookmarkAction.TouchSize = BookmarkTouchSize;

        BookmarkAction.Command = BookmarkCommand;
        BookmarkAction.CommandParameter = BookmarkCommandParameter;
    }

    #endregion

    #region Image
    public ImageSource? Image
    {
        get => (ImageSource?)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public static readonly BindableProperty ImageProperty =
        BindableProperty.Create(
            nameof(Image),
            typeof(ImageSource),
            typeof(ShowcaseCardView),
            default(ImageSource),
            propertyChanged: OnImageChanged);

    public Aspect ImageAspect
    {
        get => (Aspect)GetValue(ImageAspectProperty);
        set => SetValue(ImageAspectProperty, value);
    }

    public static readonly BindableProperty ImageAspectProperty =
        BindableProperty.Create(
            nameof(ImageAspect),
            typeof(Aspect),
            typeof(ShowcaseCardView),
            Aspect.AspectFill,
            propertyChanged: OnImageChanged);

    public double ImageOpacity
    {
        get => (double)GetValue(ImageOpacityProperty);
        set => SetValue(ImageOpacityProperty, value);
    }

    public static readonly BindableProperty ImageOpacityProperty =
        BindableProperty.Create(
            nameof(ImageOpacity),
            typeof(double),
            typeof(ShowcaseCardView),
            1d,
            propertyChanged: OnImageChanged);

    private static void OnImageChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateImage();
    }

    private void UpdateImage()
    {
        ImageView.Source = Image;
        ImageView.Aspect = ImageAspect;
        ImageView.Opacity = ImageOpacity;
    }
    #endregion

    #region ImageOverlay
    public Brush? ImageOverlayBrush
    {
        get => (Brush?)GetValue(ImageOverlayBrushProperty);
        set => SetValue(ImageOverlayBrushProperty, value);
    }

    public static readonly BindableProperty ImageOverlayBrushProperty =
        BindableProperty.Create(
            nameof(ImageOverlayBrush),
            typeof(Brush),
            typeof(ShowcaseCardView),
            default(Brush),
            propertyChanged: OnOverlayChanged);

    public bool IsImageOverlayVisible
    {
        get => (bool)GetValue(IsImageOverlayVisibleProperty);
        set => SetValue(IsImageOverlayVisibleProperty, value);
    }

    public static readonly BindableProperty IsImageOverlayVisibleProperty =
        BindableProperty.Create(
            nameof(IsImageOverlayVisible),
            typeof(bool),
            typeof(ShowcaseCardView),
            false,
            propertyChanged: OnOverlayChanged);

    private static void OnOverlayChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateOverlay();
    }

    private void UpdateOverlay()
    {
        ImageOverlay.Background = ImageOverlayBrush;
        ImageOverlay.IsVisible = IsImageOverlayVisible;
    }
    #endregion

    #region Action Layout
    public Thickness TopActionPadding
    {
        get => (Thickness)GetValue(TopActionPaddingProperty);
        set => SetValue(TopActionPaddingProperty, value);
    }

    public static readonly BindableProperty TopActionPaddingProperty =
        BindableProperty.Create(
            nameof(TopActionPadding),
            typeof(Thickness),
            typeof(ShowcaseCardView),
            new Thickness(4),
            propertyChanged: OnLayoutChanged);

    private static void OnLayoutChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateActionLayout();
    }
    public Thickness InformationPadding
    {
        get => (Thickness)GetValue(InformationPaddingProperty);
        set => SetValue(InformationPaddingProperty, value);
    }

    public static readonly BindableProperty InformationPaddingProperty =
        BindableProperty.Create(
            nameof(InformationPadding),
            typeof(Thickness),
            typeof(ShowcaseCardView),
            new Thickness(0),
            propertyChanged: OnLayoutChanged);

    private void UpdateActionLayout()
    {
        TopActionLayout.Padding = TopActionPadding;
        InformationLayout.Padding = InformationPadding;
    }
    #endregion

    #region Content
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ShowcaseCardView),
            default(string),
            propertyChanged: OnContentChanged);

    public string? Subtitle
    {
        get => (string?)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public static readonly BindableProperty SubtitleProperty =
        BindableProperty.Create(
            nameof(Subtitle),
            typeof(string),
            typeof(ShowcaseCardView),
            default(string),
            propertyChanged: OnContentChanged);


    public double Rating
    {
        get => (double)GetValue(RatingProperty);
        set => SetValue(RatingProperty, value);
    }

    public static readonly BindableProperty RatingProperty =
        BindableProperty.Create(
            nameof(Rating),
            typeof(double),
            typeof(ShowcaseCardView),
            0d,
            propertyChanged: OnContentChanged);

    private static void OnContentChanged(
    BindableObject bindable,
    object oldValue,
    object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateContent();
    }

    private void UpdateContent()
    {
        TitleLabel.Text = Title;
        SubtitleLable.Text = Subtitle;
        RatingLabel.Text = Rating.ToString("0.0");
    }
    #endregion

    #region Badge
    public string? BadgeText
    {
        get => (string?)GetValue(BadgeTextProperty);
        set => SetValue(BadgeTextProperty, value);
    }

    public static readonly BindableProperty BadgeTextProperty =
        BindableProperty.Create(
            nameof(BadgeText),
            typeof(string),
            typeof(ShowcaseCardView),
            default(string),
            propertyChanged: OnBadgeChanged);


    public Color BadgeTextColor
    {
        get => (Color)GetValue(BadgeTextColorProperty);
        set => SetValue(BadgeTextColorProperty, value);
    }

    public static readonly BindableProperty BadgeTextColorProperty =
        BindableProperty.Create(
            nameof(BadgeTextColor),
            typeof(Color),
            typeof(ShowcaseCardView),
            Colors.White,
            propertyChanged: OnBadgeChanged);


    public Brush BadgeFillBrush
    {
        get => (Brush)GetValue(BadgeFillBrushProperty);
        set => SetValue(BadgeFillBrushProperty, value);
    }

    public static readonly BindableProperty BadgeFillBrushProperty =
        BindableProperty.Create(
            nameof(BadgeFillBrush),
            typeof(Brush),
            typeof(ShowcaseCardView),
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
            typeof(ShowcaseCardView),
            10d,
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
            typeof(ShowcaseCardView),
            FontAttributes.Bold,
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
            typeof(ShowcaseCardView),
            new CornerRadius(8),
            propertyChanged: OnBadgeChanged);


    public Thickness BadgePadding
    {
        get => (Thickness)GetValue(BadgePaddingProperty);
        set => SetValue(BadgePaddingProperty, value);
    }

    public static readonly BindableProperty BadgePaddingProperty =
        BindableProperty.Create(
            nameof(BadgePadding),
            typeof(Thickness),
            typeof(ShowcaseCardView),
            new Thickness(3, 3),
            propertyChanged: OnBadgeChanged);


    private static void OnBadgeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateBadge();
    }


    private void UpdateBadge()
    {
        BadgeView.Text = BadgeText;
        BadgeView.TextColor = BadgeTextColor;
        BadgeView.FillBrush = BadgeFillBrush;
        BadgeView.FontSize = BadgeFontSize;
        BadgeView.FontAttributes = BadgeFontAttributes;
        BadgeView.CornerRadius = BadgeCornerRadius;
        BadgeView.BorderPadding = BadgePadding;
    }

    #endregion

    #region Background Image
    public ImageSource? BackgroundImage
    {
        get => (ImageSource?)GetValue(BackgroundImageProperty);
        set => SetValue(BackgroundImageProperty, value);
    }

    public static readonly BindableProperty BackgroundImageProperty =
        BindableProperty.Create(
            nameof(BackgroundImage),
            typeof(ImageSource),
            typeof(ShowcaseCardView),
            default(ImageSource),
            propertyChanged: OnCardBackgroundImageChanged);


    public Thickness BackgroundImageMargin
    {
        get => (Thickness)GetValue(BackgroundImageMarginProperty);
        set => SetValue(BackgroundImageMarginProperty, value);
    }

    public static readonly BindableProperty BackgroundImageMarginProperty =
        BindableProperty.Create(
            nameof(BackgroundImageMargin),
            typeof(Thickness),
            typeof(ShowcaseCardView),
            new Thickness(0),
            propertyChanged: OnCardBackgroundImageChanged);


    public Aspect BackgroundImageAspect
    {
        get => (Aspect)GetValue(BackgroundImageAspectProperty);
        set => SetValue(BackgroundImageAspectProperty, value);
    }

    public static readonly BindableProperty BackgroundImageAspectProperty =
        BindableProperty.Create(
            nameof(BackgroundImageAspect),
            typeof(Aspect),
            typeof(ShowcaseCardView),
            Aspect.AspectFill,
            propertyChanged: OnCardBackgroundImageChanged);


    public double BackgroundImageOpacity
    {
        get => (double)GetValue(BackgroundImageOpacityProperty);
        set => SetValue(BackgroundImageOpacityProperty, value);
    }

    public static readonly BindableProperty BackgroundImageOpacityProperty =
        BindableProperty.Create(
            nameof(BackgroundImageOpacity),
            typeof(double),
            typeof(ShowcaseCardView),
            1d,
            propertyChanged: OnCardBackgroundImageChanged);


    private static void OnCardBackgroundImageChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is ShowcaseCardView control)
            control.UpdateCardBackgroundImage();
    }


    private void UpdateCardBackgroundImage()
    {
        BackgroundImageView.Source = BackgroundImage;
        BackgroundImageView.Margin = BackgroundImageMargin;
        BackgroundImageView.Aspect = BackgroundImageAspect;
        BackgroundImageView.Opacity = BackgroundImageOpacity;
    }

    #endregion


    private void OnCardTapped(object sender, TappedEventArgs e)
    {
        ExecuteCommand();
    }
    private void ExecuteCommand()
    {
        if (TapCommand == null)
            return;

        if (TapCommand.CanExecute(TapCommandParameter))
            TapCommand.Execute(TapCommandParameter);
    }
}