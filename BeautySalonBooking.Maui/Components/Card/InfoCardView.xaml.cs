using BeautySalonBooking.Maui.Components.Shared.Enums;
using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Card;

public partial class InfoCardView : ContentView
{
    public InfoCardView()
    {
        InitializeComponent();

        UpdateBackground();
        UpdateBorder();
        UpdateCornerRadius();

        UpdateImage();
        UpdateImageShape();
        UpdateImageCornerRadius();
        UpdateImageAspect();

        UpdateTitle();
        UpdateSubtitle();
        UpdateThirdRow();
        UpdateThirdRowVisibility();

        UpdateArrowVisibility();
    }

    #region Background
    public Brush FillBrush
    {
        get => (Brush)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }
    public static readonly BindableProperty FillBrushProperty =
        BindableProperty.Create(
            nameof(FillBrush),
            typeof(Brush),
            typeof(InfoCardView),
            Brush.Transparent,
            propertyChanged: OnBackgroundChanged);

    private static void OnBackgroundChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateBackground();
    }
    private void UpdateBackground()
    {
        ContainerBorder.Background = FillBrush;
    }
    #endregion

    #region BorderBrush
    public Brush BorderBrush
    {
        get => (Brush)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(InfoCardView),
            Brush.Transparent,
            propertyChanged: OnBorderBrushChanged);

    private static void OnBorderBrushChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateBorder();
    }
    #endregion

    #region BorderWidth
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(InfoCardView),
            0d,
            propertyChanged: OnBorderWidthChanged);

    private static void OnBorderWidthChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateBorder();
    }

    private void UpdateBorder()
    {
        ContainerBorder.Stroke = BorderBrush;
        ContainerBorder.StrokeThickness = BorderWidth;
    }
    #endregion

    #region CornerRadius
    public float CornerRadius
    {
        get => (float)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(float),
            typeof(InfoCardView),
            10f,
            propertyChanged: OnCornerRadiusChanged);

    private static void OnCornerRadiusChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateCornerRadius();
    }

    private void UpdateCornerRadius()
    {
        ContainerShape.CornerRadius = CornerRadius;
    }
    #endregion

    #region ImageSource
    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(
            nameof(ImageSource),
            typeof(ImageSource),
            typeof(InfoCardView),
            default(ImageSource),
            propertyChanged: OnImageSourceChanged);

    private static void OnImageSourceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateImage();
    }

    private void UpdateImage()
    {
        ImageView.ImageSource = ImageSource;
    }
    #endregion


    #region ImageShape
    public Shape ImageShape
    {
        get => (Shape)GetValue(ImageShapeProperty);
        set => SetValue(ImageShapeProperty, value);
    }

    public static readonly BindableProperty ImageShapeProperty =
        BindableProperty.Create(
            nameof(ImageShape),
            typeof(Shape),
            typeof(InfoCardView),
            Shape.RoundedRectangle,
            propertyChanged: OnImageShapeChanged);

    private static void OnImageShapeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateImageShape();
    }

    private void UpdateImageShape()
    {
        ImageView.Shape = ImageShape;
    }
    #endregion


    #region ImageCornerRadius
    public float ImageCornerRadius
    {
        get => (float)GetValue(ImageCornerRadiusProperty);
        set => SetValue(ImageCornerRadiusProperty, value);
    }

    public static readonly BindableProperty ImageCornerRadiusProperty =
        BindableProperty.Create(
            nameof(ImageCornerRadius),
            typeof(float),
            typeof(InfoCardView),
            10f,
            propertyChanged: OnImageCornerRadiusChanged);

    private static void OnImageCornerRadiusChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateImageCornerRadius();
    }
    private void UpdateImageCornerRadius()
    {
        ImageView.CornerRadius = ImageCornerRadius;
    }
    #endregion

    #region ImageAspect
    public Aspect ImageAspect
    {
        get => (Aspect)GetValue(ImageAspectProperty);
        set => SetValue(ImageAspectProperty, value);
    }
    public static readonly BindableProperty ImageAspectProperty =
        BindableProperty.Create(
            nameof(ImageAspect),
            typeof(Aspect),
            typeof(InfoCardView),
            Aspect.AspectFill,
            propertyChanged: OnImageAspectChanged);

    private static void OnImageAspectChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateImageAspect();
    }

    private void UpdateImageAspect()
    {
        ImageView.ImageAspect = ImageAspect;
    }

    #endregion


    #region Title

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(InfoCardView),
            default(string),
            propertyChanged: OnTitleChanged);

    private static void OnTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateTitle();
    }

    private void UpdateTitle()
    {
        TitleLabel.Text = Title;
    }

    #endregion


    #region Subtitle

    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public static readonly BindableProperty SubtitleProperty =
        BindableProperty.Create(
            nameof(Subtitle),
            typeof(string),
            typeof(InfoCardView),
            default(string),
            propertyChanged: OnSubtitleChanged);

    private static void OnSubtitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateSubtitle();
    }

    private void UpdateSubtitle()
    {
        SubtitleLabel.Text = Subtitle;
    }

    #endregion


    #region ThirdRowText

    public string ThirdRowText
    {
        get => (string)GetValue(ThirdRowTextProperty);
        set => SetValue(ThirdRowTextProperty, value);
    }

    public static readonly BindableProperty ThirdRowTextProperty =
        BindableProperty.Create(
            nameof(ThirdRowText),
            typeof(string),
            typeof(InfoCardView),
            default(string),
            propertyChanged: OnThirdRowTextChanged);

    private static void OnThirdRowTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateThirdRow();
    }
    private void UpdateThirdRow()
    {
        ThirdRowLabel.Text = ThirdRowText;
        UpdateThirdRowVisibility();
    }

    #endregion


    #region IsThirdRowVisible
    public bool IsThirdRowVisible
    {
        get => (bool)GetValue(IsThirdRowVisibleProperty);
        set => SetValue(IsThirdRowVisibleProperty, value);
    }

    public static readonly BindableProperty IsThirdRowVisibleProperty =
        BindableProperty.Create(
            nameof(IsThirdRowVisible),
            typeof(bool),
            typeof(InfoCardView),
            true,
            propertyChanged: OnIsThirdRowVisibleChanged);

    private static void OnIsThirdRowVisibleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateThirdRowVisibility();
    }

    private void UpdateThirdRowVisibility()
    {
        ThirdRowLabel.IsVisible =
            IsThirdRowVisible &&
            !string.IsNullOrWhiteSpace(ThirdRowText);
    }

    #endregion


    #region IsArrowVisible
    public bool IsArrowVisible
    {
        get => (bool)GetValue(IsArrowVisibleProperty);
        set => SetValue(IsArrowVisibleProperty, value);
    }

    public static readonly BindableProperty IsArrowVisibleProperty =
        BindableProperty.Create(
            nameof(IsArrowVisible),
            typeof(bool),
            typeof(InfoCardView),
            true,
            propertyChanged: OnIsArrowVisibleChanged);

    private static void OnIsArrowVisibleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not InfoCardView control)
            return;

        control.UpdateArrowVisibility();
    }

    private void UpdateArrowVisibility()
    {
        ArrowView.IsVisible = IsArrowVisible;
    }
    #endregion

    public static readonly BindableProperty CommandProperty =
    BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(InfoCardView));

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(InfoCardView));

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
}