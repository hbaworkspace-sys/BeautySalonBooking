using System.Windows.Input;
using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.Components.Navigation;

public partial class NavigationHeaderView : ContentView
{
    public NavigationHeaderView()
    {
        InitializeComponent();

        BackButton.Clicked += OnBackClicked;
        NextButton.Clicked += OnNextClicked;

        UpdateBack();
        UpdateNext();
        UpdateTitle();
    }

    #region Back
    public bool IsBackVisible
    {
        get => (bool)GetValue(IsBackVisibleProperty);
        set => SetValue(IsBackVisibleProperty, value);
    }
    public static readonly BindableProperty IsBackVisibleProperty =
        BindableProperty.Create(
            nameof(IsBackVisible),
            typeof(bool),
            typeof(NavigationHeaderView),
            false,
            propertyChanged: OnBackChanged);


    public bool IsBackEnabled
    {
        get => (bool)GetValue(IsBackEnabledProperty);
        set => SetValue(IsBackEnabledProperty, value);
    }
    public static readonly BindableProperty IsBackEnabledProperty =
        BindableProperty.Create(
            nameof(IsBackEnabled),
            typeof(bool),
            typeof(NavigationHeaderView),
            true,
            propertyChanged: OnBackChanged);

    public string BackIcon
    {
        get => (string)GetValue(BackIconProperty);
        set => SetValue(BackIconProperty, value);
    }
    public static readonly BindableProperty BackIconProperty =
        BindableProperty.Create(
            nameof(BackIcon),
            typeof(string),
            typeof(NavigationHeaderView),
            Icon.IconSymbols.ChevronBackward,
            propertyChanged: OnBackChanged);

    public Color BackColor
    {
        get => (Color)GetValue(BackColorProperty);
        set => SetValue(BackColorProperty, value);
    }

    public static readonly BindableProperty BackColorProperty =
        BindableProperty.Create(
            nameof(BackColor),
            typeof(Color),
            typeof(NavigationHeaderView),
            Colors.Gray,
            propertyChanged: OnBackChanged);

    public double BackFontSize
    {
        get => (double)GetValue(BackFontSizeProperty);
        set => SetValue(BackFontSizeProperty, value);
    }
    public static readonly BindableProperty BackFontSizeProperty =
        BindableProperty.Create(
            nameof(BackFontSize),
            typeof(double),
            typeof(NavigationHeaderView),
            24d,
            propertyChanged: OnBackChanged);

    public ICommand? BackCommand
    {
        get => (ICommand?)GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }
    public static readonly BindableProperty BackCommandProperty =
        BindableProperty.Create(
            nameof(BackCommand),
            typeof(ICommand),
            typeof(NavigationHeaderView),
            default(ICommand));

    public object? BackCommandParameter
    {
        get => GetValue(BackCommandParameterProperty);
        set => SetValue(BackCommandParameterProperty, value);
    }
    public static readonly BindableProperty BackCommandParameterProperty =
        BindableProperty.Create(
            nameof(BackCommandParameter),
            typeof(object),
            typeof(NavigationHeaderView),
            default(object));

    private static void OnBackChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NavigationHeaderView control)
            control.UpdateBack();
    }

    private void UpdateBack()
    {
        BackButton.IsVisible = IsBackVisible;
        BackButton.IsEnabled = IsBackEnabled;

        BackButton.Text = BackIcon;
        BackButton.TextColor = BackColor;
        BackButton.FontSize = BackFontSize;
    }
    #endregion

    #region Next
    public bool IsNextVisible
    {
        get => (bool)GetValue(IsNextVisibleProperty);
        set => SetValue(IsNextVisibleProperty, value);
    }
    public static readonly BindableProperty IsNextVisibleProperty =
        BindableProperty.Create(
            nameof(IsNextVisible),
            typeof(bool),
            typeof(NavigationHeaderView),
            false,
            propertyChanged: OnNextChanged);

    public bool IsNextEnabled
    {
        get => (bool)GetValue(IsNextEnabledProperty);
        set => SetValue(IsNextEnabledProperty, value);
    }
    public static readonly BindableProperty IsNextEnabledProperty =
        BindableProperty.Create(
            nameof(IsNextEnabled),
            typeof(bool),
            typeof(NavigationHeaderView),
            true,
            propertyChanged: OnNextChanged);

    public string NextIcon
    {
        get => (string)GetValue(NextIconProperty);
        set => SetValue(NextIconProperty, value);
    }
    public static readonly BindableProperty NextIconProperty =
        BindableProperty.Create(
            nameof(NextIcon),
            typeof(string),
            typeof(NavigationHeaderView),
            Icon.IconSymbols.ChevronForward,
            propertyChanged: OnNextChanged);

    public Color NextColor
    {
        get => (Color)GetValue(NextColorProperty);
        set => SetValue(NextColorProperty, value);
    }
    public static readonly BindableProperty NextColorProperty =
        BindableProperty.Create(
            nameof(NextColor),
            typeof(Color),
            typeof(NavigationHeaderView),
            Colors.Gray,
            propertyChanged: OnNextChanged);

    public double NextFontSize
    {
        get => (double)GetValue(NextFontSizeProperty);
        set => SetValue(NextFontSizeProperty, value);
    }

    public static readonly BindableProperty NextFontSizeProperty =
        BindableProperty.Create(
            nameof(NextFontSize),
            typeof(double),
            typeof(NavigationHeaderView),
            24d,
            propertyChanged: OnNextChanged);

    public ICommand? NextCommand
    {
        get => (ICommand?)GetValue(NextCommandProperty);
        set => SetValue(NextCommandProperty, value);
    }
    public static readonly BindableProperty NextCommandProperty =
        BindableProperty.Create(
            nameof(NextCommand),
            typeof(ICommand),
            typeof(NavigationHeaderView),
            default(ICommand));

    public object? NextCommandParameter
    {
        get => GetValue(NextCommandParameterProperty);
        set => SetValue(NextCommandParameterProperty, value);
    }
    public static readonly BindableProperty NextCommandParameterProperty =
        BindableProperty.Create(
            nameof(NextCommandParameter),
            typeof(object),
            typeof(NavigationHeaderView),
            default(object));

    private static void OnNextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NavigationHeaderView control)
            control.UpdateNext();
    }

    private void UpdateNext()
    {
        NextButton.IsVisible = IsNextVisible;
        NextButton.IsEnabled = IsNextEnabled;

        NextButton.Text = NextIcon;
        NextButton.TextColor = NextColor;
        NextButton.FontSize = NextFontSize;
    }
    #endregion

    #region Title
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(NavigationHeaderView),
            default(string),
            propertyChanged: OnTitleChanged);

    public double TitleFontSize
    {
        get => (double)GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }
    public static readonly BindableProperty TitleFontSizeProperty =
        BindableProperty.Create(
            nameof(TitleFontSize),
            typeof(double),
            typeof(NavigationHeaderView),
            14d,
            propertyChanged: OnTitleChanged);

    public Color TitleTextColor
    {
        get => (Color)GetValue(TitleTextColorProperty);
        set => SetValue(TitleTextColorProperty, value);
    }
    public static readonly BindableProperty TitleTextColorProperty =
        BindableProperty.Create(
            nameof(TitleTextColor),
            typeof(Color),
            typeof(NavigationHeaderView),
            Colors.Black,
            propertyChanged: OnTitleChanged);

    public string TitleFontFamily
    {
        get => (string)GetValue(TitleFontFamilyProperty);
        set => SetValue(TitleFontFamilyProperty, value);
    }
    public static readonly BindableProperty TitleFontFamilyProperty =
        BindableProperty.Create(
            nameof(TitleFontFamily),
            typeof(string),
            typeof(NavigationHeaderView),
            "Sans",
            propertyChanged: OnTitleChanged);


    public FontAttributes TitleFontAttributes
    {
        get => (FontAttributes)GetValue(TitleFontAttributesProperty);
        set => SetValue(TitleFontAttributesProperty, value);
    }

    public static readonly BindableProperty TitleFontAttributesProperty =
        BindableProperty.Create(
            nameof(TitleFontAttributes),
            typeof(FontAttributes),
            typeof(NavigationHeaderView),
            FontAttributes.Bold,
            propertyChanged: OnTitleChanged);


    public TextAlignment TitleHorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(TitleHorizontalTextAlignmentProperty);
        set => SetValue(TitleHorizontalTextAlignmentProperty, value);
    }

    public static readonly BindableProperty TitleHorizontalTextAlignmentProperty =
        BindableProperty.Create(
            nameof(TitleHorizontalTextAlignment),
            typeof(TextAlignment),
            typeof(NavigationHeaderView),
            TextAlignment.Center,
            propertyChanged: OnTitleChanged);


    public TextAlignment TitleVerticalTextAlignment
    {
        get => (TextAlignment)GetValue(TitleVerticalTextAlignmentProperty);
        set => SetValue(TitleVerticalTextAlignmentProperty, value);
    }

    public static readonly BindableProperty TitleVerticalTextAlignmentProperty =
        BindableProperty.Create(
            nameof(TitleVerticalTextAlignment),
            typeof(TextAlignment),
            typeof(NavigationHeaderView),
            TextAlignment.Center,
            propertyChanged: OnTitleChanged);


    private static void OnTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is NavigationHeaderView control)
            control.UpdateTitle();
    }

    private void UpdateTitle()
    {
        TitleLabel.Text = Title;

        TitleLabel.FontSize = TitleFontSize;
        TitleLabel.TextColor = TitleTextColor;
        TitleLabel.FontFamily = TitleFontFamily;
        TitleLabel.FontAttributes = TitleFontAttributes;

        TitleLabel.HorizontalTextAlignment =
            TitleHorizontalTextAlignment;

        TitleLabel.VerticalTextAlignment =
            TitleVerticalTextAlignment;
    }

    #endregion


    #region Events

    private void OnBackClicked(
        object? sender,
        EventArgs e)
    {
        if (BackCommand?.CanExecute(BackCommandParameter) != true)
            return;

        BackCommand.Execute(BackCommandParameter);
    }


    private void OnNextClicked(
        object? sender,
        EventArgs e)
    {
        if (NextCommand?.CanExecute(NextCommandParameter) != true)
            return;

        NextCommand.Execute(NextCommandParameter);
    }

    #endregion
}