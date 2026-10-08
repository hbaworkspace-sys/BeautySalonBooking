using System.Windows.Input;
namespace BeautySalonBooking.Maui.Components.Profile;
public partial class ProfileStateView : ContentView
{
    public ProfileStateView() { InitializeComponent(); UpdateState(); }
    public string? Icon { get => (string?)GetValue(IconProperty); set => SetValue(IconProperty, value); }
    public static readonly BindableProperty IconProperty = CreateProperty(nameof(Icon));
    public string? Title { get => (string?)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public static readonly BindableProperty TitleProperty = CreateProperty(nameof(Title));
    public string? Message { get => (string?)GetValue(MessageProperty); set => SetValue(MessageProperty, value); }
    public static readonly BindableProperty MessageProperty = CreateProperty(nameof(Message));
    public string? ActionText { get => (string?)GetValue(ActionTextProperty); set => SetValue(ActionTextProperty, value); }
    public static readonly BindableProperty ActionTextProperty = CreateProperty(nameof(ActionText));
    public ICommand? Command { get => (ICommand?)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ProfileStateView), null, propertyChanged: OnStateChanged);
    private static BindableProperty CreateProperty(string name) => BindableProperty.Create(name, typeof(string), typeof(ProfileStateView), string.Empty, propertyChanged: OnStateChanged);
    private static void OnStateChanged(BindableObject bindable, object oldValue, object newValue) { if (bindable is ProfileStateView control) control.UpdateState(); }
    private void UpdateState() { IconLabel.Text = Icon; TitleLabel.Text = Title; MessageLabel.Text = Message; ActionButton.Text = ActionText; ActionButton.Command = Command; ActionButton.IsVisible = !string.IsNullOrWhiteSpace(ActionText) && Command is not null; }
}
