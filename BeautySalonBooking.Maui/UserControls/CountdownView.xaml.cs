using System.Windows.Input;

namespace BeautySalonBooking.Maui.UserControls;

public partial class CountdownView : ContentView
{
    private IDispatcherTimer? _timer;
    private int _remainingSeconds;

    public CountdownView()
    {
        InitializeComponent();
    }

    #region Bindable Properties
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(CountdownView), string.Empty);

    public string ActionText
    {
        get => (string)GetValue(ActionTextProperty);
        set => SetValue(ActionTextProperty, value);
    }
    public static readonly BindableProperty ActionTextProperty =
        BindableProperty.Create(nameof(ActionText), typeof(string), typeof(CountdownView), string.Empty);

    public int Duration
    {
        get => (int)GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }
    public static readonly BindableProperty DurationProperty =
        BindableProperty.Create(nameof(Duration), typeof(int), typeof(CountdownView), 120);

    public bool IsRunning
    {
        get => (bool)GetValue(IsRunningProperty);
        set => SetValue(IsRunningProperty, value);
    }
    public static readonly BindableProperty IsRunningProperty =
        BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(CountdownView), false, propertyChanged: OnIsRunningChanged);

    public ICommand? ActionCommand
    {
        get => (ICommand?)GetValue(ActionCommandProperty);
        set => SetValue(ActionCommandProperty, value);
    }
    public static readonly BindableProperty ActionCommandProperty =
        BindableProperty.Create(nameof(ActionCommand), typeof(ICommand), typeof(CountdownView));
    #endregion

    #region Countdown
    private static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CountdownView)bindable;

        if ((bool)newValue)
            control.Start();
        else
            control.Stop();
    }

    private void Start()
    {
        StopTimer();

        _remainingSeconds = Duration;

        lblTitle.IsVisible = true;
        lblTimer.IsVisible = true;
        lblAction.IsVisible = false;

        UpdateTimer();

        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    private void Stop()
    {
        StopTimer();

        lblTitle.IsVisible = false;
        lblTimer.IsVisible = false;
        lblAction.IsVisible = true;
    }

    private void StopTimer()
    {
        if (_timer == null)
            return;

        _timer.Stop();
        _timer.Tick -= OnTimerTick;
        _timer = null;
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _remainingSeconds--;

        if (_remainingSeconds <= 0)
        {
            IsRunning = false;
            return;
        }

        UpdateTimer();
    }

    private void UpdateTimer()
    {
        lblTimer.Text = TimeSpan
            .FromSeconds(_remainingSeconds)
            .ToString(@"mm\:ss");
    }
    #endregion

    #region Events
    private void OnActionTapped(object? sender, TappedEventArgs e)
    {
        if (ActionCommand?.CanExecute(null) == true)
        {
            ActionCommand.Execute(null);
        }
    }
    #endregion
}