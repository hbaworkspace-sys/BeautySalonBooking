using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Shared;

public partial class TextLink : ContentView
{
    public TextLink()
    {
        InitializeComponent();
    }

    #region QuestionText
    public string QuestionText
    {
        get => (string)GetValue(QuestionTextProperty);
        set => SetValue(QuestionTextProperty, value);
    }

    public static readonly BindableProperty QuestionTextProperty =
        BindableProperty.Create(
            nameof(QuestionText),
            typeof(string),
            typeof(TextLink),
            string.Empty);

    #endregion

    #region Text

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(TextLink),
            string.Empty);

    #endregion

    #region TextColor

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(TextLink),
            Color.FromArgb("#F05C80"));

    #endregion

    #region Command

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(TextLink));

    #endregion

    #region Click

    private void LinkLabel_Tapped(
        object? sender,
        TappedEventArgs e)
    {
        if (Command?.CanExecute(null) != true)
            return;

        Command.Execute(null);
    }

    #endregion

    #region Underline

    private void ShowUnderline()
    {
        UnderLineBox.IsVisible = true;
    }

    private void HideUnderline()
    {
        UnderLineBox.IsVisible = false;
    }

    #endregion
}