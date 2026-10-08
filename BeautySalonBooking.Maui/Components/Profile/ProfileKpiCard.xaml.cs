using System.Windows.Input;
namespace BeautySalonBooking.Maui.Components.Profile;
public partial class ProfileKpiCard : ContentView
{
    public ProfileKpiCard() { InitializeComponent(); UpdateContent(); }
    public string? Title { get => (string?)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public static readonly BindableProperty TitleProperty = CreateContentProperty(nameof(Title));
    public string? Value { get => (string?)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    public static readonly BindableProperty ValueProperty = CreateContentProperty(nameof(Value));
    public string? Detail { get => (string?)GetValue(DetailProperty); set => SetValue(DetailProperty, value); }
    public static readonly BindableProperty DetailProperty = CreateContentProperty(nameof(Detail));
    public ICommand? Command { get => (ICommand?)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ProfileKpiCard));
    public object? CommandParameter { get => GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value); }
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ProfileKpiCard));
    private static BindableProperty CreateContentProperty(string name) => BindableProperty.Create(name, typeof(string), typeof(ProfileKpiCard), string.Empty, propertyChanged: OnContentChanged);
    private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue) { if (bindable is ProfileKpiCard control) control.UpdateContent(); }
    private void UpdateContent() { TitleLabel.Text = Title; ValueLabel.Text = Value; DetailLabel.Text = Detail; DetailLabel.IsVisible = !string.IsNullOrWhiteSpace(Detail); }
}
