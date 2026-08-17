using System.Windows.Input;
using Icon = BeautySalonBooking.Maui.Resources;
namespace BeautySalonBooking.Maui.Components.Navigation;

public partial class NavigationButtonsView : ContentView
{
    public NavigationButtonsView()
    {
        InitializeComponent();
    }

    #region Behavior
    public ICommand? BackCommand
    {
        get => (ICommand?)GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }
    public static readonly BindableProperty BackCommandProperty =
        BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(NavigationButtonsView));

    public ICommand? NextCommand
    {
        get => (ICommand?)GetValue(NextCommandProperty);
        set => SetValue(NextCommandProperty, value);
    }
    public static readonly BindableProperty NextCommandProperty =
        BindableProperty.Create(nameof(NextCommand), typeof(ICommand), typeof(NavigationButtonsView));

    public object? BackCommandParameter
    {
        get => GetValue(BackCommandParameterProperty);
        set => SetValue(BackCommandParameterProperty, value);
    }
    public static readonly BindableProperty BackCommandParameterProperty =
        BindableProperty.Create(nameof(BackCommandParameter), typeof(object), typeof(NavigationButtonsView));

    public object? NextCommandParameter
    {
        get => GetValue(NextCommandParameterProperty);
        set => SetValue(NextCommandParameterProperty, value);
    }
    public static readonly BindableProperty NextCommandParameterProperty =
        BindableProperty.Create(nameof(NextCommandParameter), typeof(object), typeof(NavigationButtonsView));
    #endregion

    #region State
    public bool IsBackEnabled
    {
        get => (bool)GetValue(IsBackEnabledProperty);
        set => SetValue(IsBackEnabledProperty, value);
    }
    public static readonly BindableProperty IsBackEnabledProperty =
        BindableProperty.Create(nameof(IsBackEnabled), typeof(bool), typeof(NavigationButtonsView), true);

    public bool IsNextEnabled
    {
        get => (bool)GetValue(IsNextEnabledProperty);
        set => SetValue(IsNextEnabledProperty, value);
    }
    public static readonly BindableProperty IsNextEnabledProperty =
        BindableProperty.Create(nameof(IsNextEnabled), typeof(bool), typeof(NavigationButtonsView), true);
    #endregion

    #region Configuration
    public NavigationMode NavigationMode
    {
        get => (NavigationMode)GetValue(NavigationModeProperty);
        set => SetValue(NavigationModeProperty, value);
    }
    public static readonly BindableProperty NavigationModeProperty =
        BindableProperty.Create(nameof(NavigationMode), typeof(NavigationMode), typeof(NavigationButtonsView), NavigationMode.Both, propertyChanged: OnNavigationModeChanged);
    private static void OnNavigationModeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not NavigationButtonsView control)
            return;

        control.OnPropertyChanged(nameof(IsBackVisible));
        control.OnPropertyChanged(nameof(IsNextVisible));
    }
    #endregion

    #region Computed Properties
    public bool IsBackVisible =>
    NavigationMode is NavigationMode.Back or NavigationMode.Both;
    public bool IsNextVisible =>
        NavigationMode is NavigationMode.Next or NavigationMode.Both;
    #endregion

    #region Appearance
    public string BackIcon
    {
        get => (string)GetValue(BackIconProperty);
        set => SetValue(BackIconProperty, value);
    }
    public static readonly BindableProperty BackIconProperty =
        BindableProperty.Create(nameof(BackIcon), typeof(string), typeof(NavigationButtonsView), Icon.IconSymbols.ChevronBackward);
    public string NextIcon
    {
        get => (string)GetValue(NextIconProperty);
        set => SetValue(NextIconProperty, value);
    }
    public static readonly BindableProperty NextIconProperty =
        BindableProperty.Create(nameof(NextIcon), typeof(string), typeof(NavigationButtonsView), Icon.IconSymbols.ChevronForward);

    public Color BackColor
    {
        get => (Color)GetValue(BackColorProperty);
        set => SetValue(BackColorProperty, value);
    }
    public static readonly BindableProperty BackColorProperty =
        BindableProperty.Create(nameof(BackColor), typeof(Color), typeof(NavigationButtonsView), Colors.Black);
    public Color NextColor
    {
        get => (Color)GetValue(NextColorProperty);
        set => SetValue(NextColorProperty, value);
    }
    public static readonly BindableProperty NextColorProperty =
        BindableProperty.Create(nameof(NextColor), typeof(Color), typeof(NavigationButtonsView), Colors.Black);

    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    public static readonly BindableProperty IconSizeProperty =
        BindableProperty.Create(nameof(IconSize), typeof(double), typeof(NavigationButtonsView), 35d);
    #endregion
}