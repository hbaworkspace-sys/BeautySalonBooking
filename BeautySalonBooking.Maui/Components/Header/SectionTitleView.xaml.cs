namespace BeautySalonBooking.Maui.Components.Header;

public partial class SectionTitleView : ContentView
{
    public SectionTitleView()
    {
        InitializeComponent();

        UpdateTitle();
        UpdateDividerVisibility();
    }

    #region Title
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(SectionTitleView),
            default(string),
            propertyChanged: OnTitleChanged);

    private static void OnTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not SectionTitleView control)
            return;

        control.UpdateTitle();
    }

    private void UpdateTitle()
    {
        TitleLabel.Text = Title;
    }
    #endregion

    #region ShowDivider
    public bool ShowDivider
    {
        get => (bool)GetValue(ShowDividerProperty);
        set => SetValue(ShowDividerProperty, value);
    }

    public static readonly BindableProperty ShowDividerProperty =
        BindableProperty.Create(
            nameof(ShowDivider),
            typeof(bool),
            typeof(SectionTitleView),
            false,
            propertyChanged: OnShowDividerChanged);

    private static void OnShowDividerChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not SectionTitleView control)
            return;

        control.UpdateDividerVisibility();
    }

    private void UpdateDividerVisibility()
    {
        Divider.IsVisible = ShowDivider;
    }
    #endregion
}