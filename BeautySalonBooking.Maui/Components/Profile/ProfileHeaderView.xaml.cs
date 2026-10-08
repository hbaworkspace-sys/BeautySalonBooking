using System.Windows.Input;
namespace BeautySalonBooking.Maui.Components.Profile;
public partial class ProfileHeaderView : ContentView
{
    public ProfileHeaderView() { InitializeComponent(); UpdateContent(); }
    public string? Title { get => (string?)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ProfileHeaderView), string.Empty, propertyChanged: OnContentChanged);
    public string? ActionText { get => (string?)GetValue(ActionTextProperty); set => SetValue(ActionTextProperty, value); }
    public static readonly BindableProperty ActionTextProperty = BindableProperty.Create(nameof(ActionText), typeof(string), typeof(ProfileHeaderView), string.Empty, propertyChanged: OnContentChanged);
    public ICommand? BackCommand { get => (ICommand?)GetValue(BackCommandProperty); set => SetValue(BackCommandProperty, value); }
    public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(ProfileHeaderView));
    private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue) { if (bindable is ProfileHeaderView control) control.UpdateContent(); }
    private void UpdateContent() { TitleLabel.Text = Title; ActionLabel.Text = ActionText; ActionLabel.IsVisible = !string.IsNullOrWhiteSpace(ActionText); }
}
