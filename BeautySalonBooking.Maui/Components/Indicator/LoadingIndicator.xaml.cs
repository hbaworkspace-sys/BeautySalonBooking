namespace BeautySalonBooking.Maui.Components.Indicator;

public partial class LoadingIndicator : ContentView
{
    public LoadingIndicator()
    {
        InitializeComponent();
        InitializeControl();
    }

    private void InitializeControl()
    {
        UpdateLoading();
        UpdateOverlay();
        UpdateSpinner();
        UpdateMessage();
    }

    #region Loading

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(LoadingIndicator),
            false,
            propertyChanged: OnLoadingChanged);

    private static void OnLoadingChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not LoadingIndicator control)
            return;

        control.UpdateLoading();
    }

    private void UpdateLoading()
    {
        IsVisible = IsLoading;
        LoadingIndicatorView.IsRunning = IsLoading;
    }

    #endregion

    #region Message

    public string? Message
    {
        get => (string?)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public static readonly BindableProperty MessageProperty =
        BindableProperty.Create(
            nameof(Message),
            typeof(string),
            typeof(LoadingIndicator),
            "لطفاً صبر کنید...",
            propertyChanged: OnMessageChanged);

    public bool ShowMessage
    {
        get => (bool)GetValue(ShowMessageProperty);
        set => SetValue(ShowMessageProperty, value);
    }

    public static readonly BindableProperty ShowMessageProperty =
        BindableProperty.Create(
            nameof(ShowMessage),
            typeof(bool),
            typeof(LoadingIndicator),
            true,
            propertyChanged: OnMessageChanged);

    public double MessageSize
    {
        get => (double)GetValue(MessageSizeProperty);
        set => SetValue(MessageSizeProperty, value);
    }

    public static readonly BindableProperty MessageSizeProperty =
        BindableProperty.Create(
            nameof(MessageSize),
            typeof(double),
            typeof(LoadingIndicator),
            14d,
            propertyChanged: OnMessageChanged);

    public Color MessageColor
    {
        get => (Color)GetValue(MessageColorProperty);
        set => SetValue(MessageColorProperty, value);
    }

    public static readonly BindableProperty MessageColorProperty =
        BindableProperty.Create(
            nameof(MessageColor),
            typeof(Color),
            typeof(LoadingIndicator),
            Colors.White,
            propertyChanged: OnMessageChanged);

    private static void OnMessageChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not LoadingIndicator control)
            return;

        control.UpdateMessage();
    }

    private void UpdateMessage()
    {
        MessageLabel.Text = Message;
        MessageLabel.IsVisible = ShowMessage;
        MessageLabel.FontSize = MessageSize;
        MessageLabel.TextColor = MessageColor;
    }

    #endregion

    #region Overlay

    public Color OverlayColor
    {
        get => (Color)GetValue(OverlayColorProperty);
        set => SetValue(OverlayColorProperty, value);
    }

    public static readonly BindableProperty OverlayColorProperty =
        BindableProperty.Create(
            nameof(OverlayColor),
            typeof(Color),
            typeof(LoadingIndicator),
            Color.FromArgb("#CCFFFFFF"),
            propertyChanged: OnOverlayChanged);

    private static void OnOverlayChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not LoadingIndicator control)
            return;

        control.UpdateOverlay();
    }

    private void UpdateOverlay()
    {
        OverlayBorder.BackgroundColor = OverlayColor;
    }

    #endregion

    #region Spinner

    public Color SpinnerColor
    {
        get => (Color)GetValue(SpinnerColorProperty);
        set => SetValue(SpinnerColorProperty, value);
    }

    public static readonly BindableProperty SpinnerColorProperty =
        BindableProperty.Create(
            nameof(SpinnerColor),
            typeof(Color),
            typeof(LoadingIndicator),
            Colors.Black,
            propertyChanged: OnSpinnerChanged);

    public double SpinnerSize
    {
        get => (double)GetValue(SpinnerSizeProperty);
        set => SetValue(SpinnerSizeProperty, value);
    }

    public static readonly BindableProperty SpinnerSizeProperty =
        BindableProperty.Create(
            nameof(SpinnerSize),
            typeof(double),
            typeof(LoadingIndicator),
            45d,
            propertyChanged: OnSpinnerChanged);

    private static void OnSpinnerChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not LoadingIndicator control)
            return;

        control.UpdateSpinner();
    }

    private void UpdateSpinner()
    {
        LoadingIndicatorView.Color = SpinnerColor;
        LoadingIndicatorView.WidthRequest = SpinnerSize;
        LoadingIndicatorView.HeightRequest = SpinnerSize;
    }

    #endregion
}