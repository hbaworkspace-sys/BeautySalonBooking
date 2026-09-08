using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Header;

public partial class SectionHeaderView : ContentView
{
    public SectionHeaderView()
    {
        InitializeComponent();

        ActionTapGesture.Tapped += OnActionTapped;

        UpdateTitle();
        UpdateActionText();
        UpdateActionVisibility();
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
            typeof(SectionHeaderView),
            default(string),
            propertyChanged: OnTitleChanged);
    private static void OnTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not SectionHeaderView control)
            return;

        control.UpdateTitle();
    }
    private void UpdateTitle()
    {
        TitleLabel.Text = Title;
    }
    #endregion


    #region ActionText
    public string ActionText
    {
        get => (string)GetValue(ActionTextProperty);
        set => SetValue(ActionTextProperty, value);
    }
    public static readonly BindableProperty ActionTextProperty =
        BindableProperty.Create(
            nameof(ActionText),
            typeof(string),
            typeof(SectionHeaderView),
            default(string),
            propertyChanged: OnActionTextChanged);
    private static void OnActionTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not SectionHeaderView control)
            return;

        control.UpdateActionText();
        control.UpdateActionVisibility();
    }
    private void UpdateActionText()
    {
        ActionLabel.Text = ActionText;
    }
    #endregion


    #region IsActionVisible
    public bool IsActionVisible
    {
        get => (bool)GetValue(IsActionVisibleProperty);
        set => SetValue(IsActionVisibleProperty, value);
    }
    public static readonly BindableProperty IsActionVisibleProperty =
        BindableProperty.Create(
            nameof(IsActionVisible),
            typeof(bool),
            typeof(SectionHeaderView),
            true,
            propertyChanged: OnIsActionVisibleChanged);
    private static void OnIsActionVisibleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not SectionHeaderView control)
            return;

        control.UpdateActionVisibility();
    }
    private void UpdateActionVisibility()
    {
        ActionLabel.IsVisible =
            IsActionVisible &&
            !string.IsNullOrWhiteSpace(ActionText);
    }
    #endregion


    #region ActionCommand
    public ICommand ActionCommand
    {
        get => (ICommand)GetValue(ActionCommandProperty);
        set => SetValue(ActionCommandProperty, value);
    }
    public static readonly BindableProperty ActionCommandProperty =
        BindableProperty.Create(
            nameof(ActionCommand),
            typeof(ICommand),
            typeof(SectionHeaderView),
            default(ICommand));
    #endregion


    #region ActionCommandParameter
    public object ActionCommandParameter
    {
        get => GetValue(ActionCommandParameterProperty);
        set => SetValue(ActionCommandParameterProperty, value);
    }
    public static readonly BindableProperty ActionCommandParameterProperty =
        BindableProperty.Create(
            nameof(ActionCommandParameter),
            typeof(object),
            typeof(SectionHeaderView),
            default(object));
    #endregion

    #region Action
    private void OnActionTapped(object? sender, TappedEventArgs e)
    {
        if (!IsActionVisible)
            return;

        if (ActionCommand?.CanExecute(ActionCommandParameter) != true)
            return;

        ActionCommand.Execute(ActionCommandParameter);
    }
    #endregion
}