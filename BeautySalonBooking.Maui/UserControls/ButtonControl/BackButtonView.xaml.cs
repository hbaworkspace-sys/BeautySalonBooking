using System.Windows.Input;

namespace BeautySalonBooking.Maui.UserControls.ButtonControl;

public partial class NextAndBackButtonView : ContentView
{
    public event EventHandler OnBackButtonClick;
    public event EventHandler OnNextButtonClick;

    public NextAndBackButtonView()
    {
        InitializeComponent();
    }

    public bool IsBackVisible
    {
        get => (bool)GetValue(IsBackVisibleProperty);
        set => SetValue(IsBackVisibleProperty, value);
    }
    public static readonly BindableProperty IsBackVisibleProperty =
            BindableProperty.Create(nameof(IsBackVisible), typeof(bool), typeof(NextAndBackButtonView), false);

    public bool IsNextVisible
    {
        get => (bool)GetValue(IsNextVisibleProperty);
        set => SetValue(IsNextVisibleProperty, value);
    }
    public static readonly BindableProperty IsNextVisibleProperty =
            BindableProperty.Create(nameof(IsNextVisible), typeof(bool), typeof(NextAndBackButtonView), false);

    public ICommand BackCommand
    {
        get => (ICommand)GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }
    public static readonly BindableProperty BackCommandProperty =
            BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(NextAndBackButtonView), default(ICommand));

    public ICommand NextCommand
    {
        get => (ICommand)GetValue(NextCommandProperty);
        set => SetValue(NextCommandProperty, value);
    }
    public static readonly BindableProperty NextCommandProperty =
            BindableProperty.Create(nameof(NextCommand), typeof(ICommand), typeof(NextAndBackButtonView), default(ICommand));

    private async void btnBack_Clicked(object sender, EventArgs e)
    {
        if (BackCommand?.CanExecute(null) == true)
        {
            BackCommand.Execute(null);
            return;
        }
        await Shell.Current.GoToAsync("..");
    }

    private void btnNext_Clicked(object sender, EventArgs e)
    {
        OnNextButtonClick?.Invoke(sender, e);
    }
}