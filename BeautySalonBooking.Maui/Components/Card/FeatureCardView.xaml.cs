using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Card;

public partial class FeatureCardView : ContentView
{
    public FeatureCardView()
    {
        InitializeComponent();

        UpdateLayoutProperties();
        UpdateAppearance();
        UpdateImage();
        UpdateOverlay();
        UpdateContent();
        UpdateAction();
        UpdateShadow();
        HeightRequest = 190;
        WidthRequest = 125;
    }

    #region Layout
    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(FeatureCardView),
            20d,
            propertyChanged: OnLayoutChanged);

    public Thickness ContentPadding
    {
        get => (Thickness)GetValue(ContentPaddingProperty);
        set => SetValue(ContentPaddingProperty, value);
    }
    public static readonly BindableProperty ContentPaddingProperty =
        BindableProperty.Create(
            nameof(ContentPadding),
            typeof(Thickness),
            typeof(FeatureCardView),
            new Thickness(10),
            propertyChanged: OnLayoutChanged);

    private static void OnLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is FeatureCardView control)
            control.UpdateLayoutProperties();
    }

    private void UpdateLayoutProperties()
    {
        CardShape.CornerRadius = new CornerRadius(CornerRadius);
        ContentLayout.Padding = ContentPadding;
    }
    #endregion

    #region Appearance
    public Brush? FillBrush
    {
        get => (Brush?)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }
    public static readonly BindableProperty FillBrushProperty =
        BindableProperty.Create(
            nameof(FillBrush),
            typeof(Brush),
            typeof(FeatureCardView),
            null,
            propertyChanged: OnAppearanceChanged);

    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }
    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(FeatureCardView),
            null,
            propertyChanged: OnAppearanceChanged);

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }
    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(FeatureCardView),
            0d,
            propertyChanged: OnAppearanceChanged);

    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is FeatureCardView control)
            control.UpdateAppearance();
    }
    private void UpdateAppearance()
    {
        ContainerBorder.Background = FillBrush;
        ContainerBorder.Stroke = BorderBrush;
        ContainerBorder.StrokeThickness = BorderWidth;
    }
    #endregion

    #region Shadow
    public Shadow? CardShadow
    {
        get => (Shadow?)GetValue(CardShadowProperty);
        set => SetValue(CardShadowProperty, value);
    }
    public static readonly BindableProperty CardShadowProperty =
        BindableProperty.Create(
            nameof(CardShadow),
            typeof(Shadow),
            typeof(FeatureCardView),
            null,
            propertyChanged: OnShadowChanged);

    private static void OnShadowChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is FeatureCardView control)
            control.UpdateShadow();
    }
    private void UpdateShadow()
    {
        ContainerBorder.Shadow = CardShadow;
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
            typeof(FeatureCardView),
            null,
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
            typeof(FeatureCardView),
            Aspect.AspectFill,
            propertyChanged: OnImageChanged);

    private static void OnImageChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is FeatureCardView control)
            control.UpdateImage();
    }
    private void UpdateImage()
    {
        ImageView.Source = Image;
        ImageView.Aspect = ImageAspect;
    }
    #endregion

    #region Overlay
    public Brush? OverlayBrush
    {
        get => (Brush?)GetValue(OverlayBrushProperty);
        set => SetValue(OverlayBrushProperty, value);
    }
    public static readonly BindableProperty OverlayBrushProperty =
        BindableProperty.Create(
            nameof(OverlayBrush),
            typeof(Brush),
            typeof(FeatureCardView),
            null,
            propertyChanged: OnOverlayChanged);

    public bool IsOverlayVisible
    {
        get => (bool)GetValue(IsOverlayVisibleProperty);
        set => SetValue(IsOverlayVisibleProperty, value);
    }
    public static readonly BindableProperty IsOverlayVisibleProperty =
        BindableProperty.Create(
            nameof(IsOverlayVisible),
            typeof(bool),
            typeof(FeatureCardView),
            false,
            propertyChanged: OnOverlayChanged);

    private static void OnOverlayChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is FeatureCardView control)
            control.UpdateOverlay();
    }
    private void UpdateOverlay()
    {
        OverlayView.Background = OverlayBrush;
        OverlayView.IsVisible = IsOverlayVisible;
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
            typeof(FeatureCardView),
            "عنوان",
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
            typeof(FeatureCardView),
            "توضیحات",
            propertyChanged: OnContentChanged);

    private static void OnContentChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is FeatureCardView control)
            control.UpdateContent();
    }
    private void UpdateContent()
    {
        TitleLabel.Text = Title;
        SubtitleLabel.Text = Subtitle;
    }
    #endregion

    #region Action
    public string? ActionText
    {
        get => (string?)GetValue(ActionTextProperty);
        set => SetValue(ActionTextProperty, value);
    }
    public static readonly BindableProperty ActionTextProperty =
        BindableProperty.Create(
            nameof(ActionText),
            typeof(string),
            typeof(FeatureCardView),
            "مشاهده",
            propertyChanged: OnActionChanged);

    public Brush? ActionBackground
    {
        get => (Brush?)GetValue(ActionBackgroundProperty);
        set => SetValue(ActionBackgroundProperty, value);
    }
    public static readonly BindableProperty ActionBackgroundProperty =
        BindableProperty.Create(
            nameof(ActionBackground),
            typeof(Brush),
            typeof(FeatureCardView),
           new SolidColorBrush(Colors.Pink),
            propertyChanged: OnActionChanged);

    public ICommand? ActionCommand
    {
        get => (ICommand?)GetValue(ActionCommandProperty);
        set => SetValue(ActionCommandProperty, value);
    }
    public static readonly BindableProperty ActionCommandProperty =
        BindableProperty.Create(
            nameof(ActionCommand),
            typeof(ICommand),
            typeof(FeatureCardView),
            null);

    public object? ActionCommandParameter
    {
        get => GetValue(ActionCommandParameterProperty);
        set => SetValue(ActionCommandParameterProperty, value);
    }
    public static readonly BindableProperty ActionCommandParameterProperty =
        BindableProperty.Create(
            nameof(ActionCommandParameter),
            typeof(object),
            typeof(FeatureCardView),
            null);

    private static void OnActionChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is FeatureCardView control)
            control.UpdateAction();
    }
    private void UpdateAction()
    {
        ActionLabel.Text = ActionText;
        ActionBorder.Background = ActionBackground;
    }
    #endregion

    #region Card Command
    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(FeatureCardView),
            null);

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(FeatureCardView),
            null);
    #endregion

    private void OnCardTapped(object sender, TappedEventArgs e)
    {
        Execute(Command, CommandParameter);
    }
    private void OnActionTapped(object sender, TappedEventArgs e)
    {
        Execute(ActionCommand, ActionCommandParameter);
    }
    private void Execute(
        ICommand? command,
        object? parameter)
    {
        if (command == null)
            return;

        if (command.CanExecute(parameter))
            command.Execute(parameter);
    }

}