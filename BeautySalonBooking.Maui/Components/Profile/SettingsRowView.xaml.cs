using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Profile;

public partial class SettingsRowView : ContentView
{
    public SettingsRowView()
    {
        InitializeComponent();
        UpdateContent();
        UpdateAppearance();
    }

    public string? Icon
    {
        get => (string?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    public static readonly BindableProperty IconProperty =
        CreateContentProperty(nameof(Icon));
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly BindableProperty TitleProperty =
        CreateContentProperty(nameof(Title));
    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    public static readonly BindableProperty DescriptionProperty =
        CreateContentProperty(nameof(Description));
    public string? Value
    {
        get => (string?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    public static readonly BindableProperty ValueProperty =
        CreateContentProperty(nameof(Value));

    public string ActionTitle
    {
        get => (string)GetValue(ActionTitleProperty);
        set => SetValue(ActionTitleProperty, value);
    }
    public static readonly BindableProperty ActionTitleProperty =
      BindableProperty.Create(
            nameof(ActionTitle),
            typeof(string),
            typeof(SettingsRowView),
            "‹",
            propertyChanged: OnContentChanged);

    public bool IsDestructive
    {
        get => (bool)GetValue(IsDestructiveProperty);
        set => SetValue(IsDestructiveProperty, value);
    }
    public static readonly BindableProperty IsDestructiveProperty =
        BindableProperty.Create(
            nameof(IsDestructive),
            typeof(bool),
            typeof(SettingsRowView),
            false,
            propertyChanged: OnAppearanceChanged);

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(SettingsRowView));
    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(SettingsRowView));

    private static BindableProperty CreateContentProperty(string name) =>
        BindableProperty.Create(
            name,
            typeof(string),
            typeof(SettingsRowView),
            string.Empty,
            propertyChanged: OnContentChanged);

    private static void OnContentChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SettingsRowView control)
            control.UpdateContent();
    }
    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SettingsRowView control)
            control.UpdateAppearance();
    }
    private void UpdateContent()
    {
        IconLabel.Text = Icon;
        TitleLabel.Text = Title;
        DescriptionLabel.Text = Description;
        DescriptionLabel.IsVisible = !string.IsNullOrWhiteSpace(Description);
        ValueLabel.Text = Value;
        ActionTitleLable.Text = ActionTitle;
    }
    private void UpdateAppearance()
    {
        var color = IsDestructive ? Color.FromArgb("#D64550") : Color.FromArgb("#ED5F91");
        IconContainer.Background = IsDestructive ? Color.FromArgb("#FFF0F1") : Color.FromArgb("#FFF0F5");
        IconLabel.TextColor = color;
        TitleLabel.TextColor = IsDestructive ? color : Color.FromArgb("#29242A");
        IconContainer.Background = IconBackgroundBrush;
        IconLabel.TextColor = IconTextColor;
    }

    public Color? IconTextColor
    {
        get => (Color?)GetValue(IconTextColorProperty);
        set => SetValue(IconTextColorProperty, value);
    }
    public static readonly BindableProperty IconTextColorProperty =
            BindableProperty.Create(
                nameof(IconTextColor),
                typeof(Color),
                typeof(SettingsRowView),
                Color.FromArgb("#ED7F89"),
                propertyChanged: OnIconTextColorChanged);

    private static void OnIconTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SettingsRowView control)
            control.UpdateAppearance();
    }

    public Brush IconBackgroundBrush
    {
        get => (Brush)GetValue(IconBackgroundColorProperty);
        set => SetValue(IconBackgroundColorProperty, value);
    }
    public static readonly BindableProperty IconBackgroundColorProperty =
            BindableProperty.Create(
                nameof(IconBackgroundBrush),
                typeof(Brush),
                typeof(SettingsRowView),
                new SolidColorBrush(Color.FromArgb("#FFF0F5")),
                propertyChanged: OnIconBackgroundColorChanged);

    private static void OnIconBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SettingsRowView control)
            control.UpdateAppearance();
    }

    public bool IsIconVisible
    {
        get => (bool)GetValue(IsIconVisibleProperty);
        set => SetValue(IsIconVisibleProperty, value);
    }
    public static readonly BindableProperty IsIconVisibleProperty =
            BindableProperty.Create(nameof(IsIconVisible), typeof(bool), typeof(SettingsRowView), true,propertyChanged: OnIsIconVisibleChanged);

    private static void OnIsIconVisibleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is SettingsRowView control)
            control.UpdateIconVisibility();
    }
    private void UpdateIconVisibility()
    {
        IconContainer.IsVisible = IsIconVisible;
    }
}
