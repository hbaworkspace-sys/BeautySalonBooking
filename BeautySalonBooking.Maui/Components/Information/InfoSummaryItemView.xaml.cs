namespace BeautySalonBooking.Maui.Components.Information;

public partial class InfoSummaryItemView : ContentView
{
    public InfoSummaryItemView()
    {
        InitializeComponent();

        UpdateColumn1();
        UpdateColumn2();
        UpdateColumn3();
        UpdateSeparatorVisibility();
    }

    #region Column 1

    public static readonly BindableProperty Column1Property =
        BindableProperty.Create(
            nameof(Column1),
            typeof(string),
            typeof(InfoSummaryItemView),
            string.Empty,
            propertyChanged: OnColumn1Changed);

    public string Column1
    {
        get => (string)GetValue(Column1Property);
        set => SetValue(Column1Property, value);
    }

    private static void OnColumn1Changed(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoSummaryItemView view)
            view.UpdateColumn1();
    }

    private void UpdateColumn1()
    {
        Column1Label.Text = Column1;
    }

    #endregion


    #region Column 2

    public static readonly BindableProperty Column2Property =
        BindableProperty.Create(
            nameof(Column2),
            typeof(string),
            typeof(InfoSummaryItemView),
            string.Empty,
            propertyChanged: OnColumn2Changed);

    public string Column2
    {
        get => (string)GetValue(Column2Property);
        set => SetValue(Column2Property, value);
    }

    private static void OnColumn2Changed(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoSummaryItemView view)
            view.UpdateColumn2();
    }

    private void UpdateColumn2()
    {
        Column2Label.Text = Column2;
    }

    #endregion


    #region Column 3

    public static readonly BindableProperty Column3Property =
        BindableProperty.Create(
            nameof(Column3),
            typeof(string),
            typeof(InfoSummaryItemView),
            string.Empty,
            propertyChanged: OnColumn3Changed);

    public string Column3
    {
        get => (string)GetValue(Column3Property);
        set => SetValue(Column3Property, value);
    }

    private static void OnColumn3Changed(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoSummaryItemView view)
            view.UpdateColumn3();
    }

    private void UpdateColumn3()
    {
        Column3Label.Text = Column3;
    }

    #endregion


    #region Separator

    public static readonly BindableProperty IsSeparatorVisibleProperty =
        BindableProperty.Create(
            nameof(IsSeparatorVisible),
            typeof(bool),
            typeof(InfoSummaryItemView),
            true,
            propertyChanged: OnIsSeparatorVisibleChanged);

    public bool IsSeparatorVisible
    {
        get => (bool)GetValue(IsSeparatorVisibleProperty);
        set => SetValue(IsSeparatorVisibleProperty, value);
    }

    private static void OnIsSeparatorVisibleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoSummaryItemView view)
            view.UpdateSeparatorVisibility();
    }

    private void UpdateSeparatorVisibility()
    {
        Separator.IsVisible = IsSeparatorVisible;
    }

    #endregion
}