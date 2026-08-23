namespace BeautySalonBooking.Maui.Components.Shared;

public partial class TitleDescriptionView : ContentView
{
    public TitleDescriptionView()
    {
        InitializeComponent();

        UpdateAll();
    }

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
            typeof(TitleDescriptionView),
            null,
            propertyChanged: OnContentChanged);

    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(
            nameof(Description),
            typeof(string),
            typeof(TitleDescriptionView),
            null,
            propertyChanged: OnContentChanged);

    private static void OnContentChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TitleDescriptionView control)
            control.UpdateContent();
    }

    private void UpdateContent()
    {
        TitleLabel.Text = Title;
        DescriptionLabel.Text = Description;
    }

    #endregion


    #region Appearance

    public Color TitleColor
    {
        get => (Color)GetValue(TitleColorProperty);
        set => SetValue(TitleColorProperty, value);
    }

    public static readonly BindableProperty TitleColorProperty =
        BindableProperty.Create(
            nameof(TitleColor),
            typeof(Color),
            typeof(TitleDescriptionView),
            Color.FromArgb("#7d0a26"),
            propertyChanged: OnAppearanceChanged);

    public Color DescriptionColor
    {
        get => (Color)GetValue(DescriptionColorProperty);
        set => SetValue(DescriptionColorProperty, value);
    }

    public static readonly BindableProperty DescriptionColorProperty =
        BindableProperty.Create(
            nameof(DescriptionColor),
            typeof(Color),
            typeof(TitleDescriptionView),
            Color.FromArgb("#87767B"),
            propertyChanged: OnAppearanceChanged);

    public double TitleFontSize
    {
        get => (double)GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }

    public static readonly BindableProperty TitleFontSizeProperty =
        BindableProperty.Create(
            nameof(TitleFontSize),
            typeof(double),
            typeof(TitleDescriptionView),
            20d,
            propertyChanged: OnAppearanceChanged);

    public double DescriptionFontSize
    {
        get => (double)GetValue(DescriptionFontSizeProperty);
        set => SetValue(DescriptionFontSizeProperty, value);
    }

    public static readonly BindableProperty DescriptionFontSizeProperty =
        BindableProperty.Create(
            nameof(DescriptionFontSize),
            typeof(double),
            typeof(TitleDescriptionView),
            11d,
            propertyChanged: OnAppearanceChanged);

    public string? TitleFontFamily
    {
        get => (string?)GetValue(TitleFontFamilyProperty);
        set => SetValue(TitleFontFamilyProperty, value);
    }

    public static readonly BindableProperty TitleFontFamilyProperty =
        BindableProperty.Create(
            nameof(TitleFontFamily),
            typeof(string),
            typeof(TitleDescriptionView),
            "Sans",
            propertyChanged: OnAppearanceChanged);

    public string? DescriptionFontFamily
    {
        get => (string?)GetValue(DescriptionFontFamilyProperty);
        set => SetValue(DescriptionFontFamilyProperty, value);
    }

    public static readonly BindableProperty DescriptionFontFamilyProperty =
        BindableProperty.Create(
            nameof(DescriptionFontFamily),
            typeof(string),
            typeof(TitleDescriptionView),
            "Sans",
            propertyChanged: OnAppearanceChanged);

    public FontAttributes TitleFontAttributes
    {
        get => (FontAttributes)GetValue(TitleFontAttributesProperty);
        set => SetValue(TitleFontAttributesProperty, value);
    }

    public static readonly BindableProperty TitleFontAttributesProperty =
        BindableProperty.Create(
            nameof(TitleFontAttributes),
            typeof(FontAttributes),
            typeof(TitleDescriptionView),
            FontAttributes.Bold,
            propertyChanged: OnAppearanceChanged);

    public FontAttributes DescriptionFontAttributes
    {
        get => (FontAttributes)GetValue(DescriptionFontAttributesProperty);
        set => SetValue(DescriptionFontAttributesProperty, value);
    }

    public static readonly BindableProperty DescriptionFontAttributesProperty =
        BindableProperty.Create(
            nameof(DescriptionFontAttributes),
            typeof(FontAttributes),
            typeof(TitleDescriptionView),
            FontAttributes.Bold,
            propertyChanged: OnAppearanceChanged);

    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TitleDescriptionView control)
            control.UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        TitleLabel.TextColor = TitleColor;
        TitleLabel.FontSize = TitleFontSize;
        TitleLabel.FontFamily = TitleFontFamily;
        TitleLabel.FontAttributes = TitleFontAttributes;

        DescriptionLabel.TextColor = DescriptionColor;
        DescriptionLabel.FontSize = DescriptionFontSize;
        DescriptionLabel.FontFamily = DescriptionFontFamily;
        DescriptionLabel.FontAttributes = DescriptionFontAttributes;
    }

    #endregion


    #region Layout

    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public static readonly BindableProperty SpacingProperty =
        BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(TitleDescriptionView),
            8d,
            propertyChanged: OnLayoutChanged);

    private static void OnLayoutChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is TitleDescriptionView control)
            control.UpdateLayout();
    }

    private void UpdateLayout()
    {
        Container.Spacing = Spacing;
    }

    #endregion


    #region General

    private void UpdateAll()
    {
        UpdateContent();
        UpdateAppearance();
        UpdateLayout();
    }

    #endregion
}