using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Card;

public partial class InfoActionCardView : ContentView
{
    public InfoActionCardView()
    {
        InitializeComponent();

        UpdateTitle();
        UpdateDescription();
        UpdateStatus();
        UpdateAction();
        UpdateVisibility();
        UpdateVisualState();
        UpdateIconGlyph();
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
            typeof(InfoActionCardView),
            string.Empty,
            propertyChanged: OnTitleChanged);


    private static void OnTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoActionCardView control)
            control.UpdateTitle();
    }

    private void UpdateTitle()
    {
        TitleLabel.Text = Title;
    }

    #endregion

    #region Description

    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(
            nameof(Description),
            typeof(string),
            typeof(InfoActionCardView),
            string.Empty,
            propertyChanged: OnDescriptionChanged);

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    private static void OnDescriptionChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoActionCardView control)
            control.UpdateDescription();
    }

    private void UpdateDescription()
    {
        DescriptionLabel.Text = Description;
        DescriptionLabel.IsVisible = !string.IsNullOrWhiteSpace(Description);
    }

    #endregion

    #region Status

    public static readonly BindableProperty StatusTextProperty =
        BindableProperty.Create(
            nameof(StatusText),
            typeof(string),
            typeof(InfoActionCardView),
            string.Empty,
            propertyChanged: OnStatusChanged);

    public string StatusText
    {
        get => (string)GetValue(StatusTextProperty);
        set => SetValue(StatusTextProperty, value);
    }


    public static readonly BindableProperty IsStatusVisibleProperty =
        BindableProperty.Create(
            nameof(IsStatusVisible),
            typeof(bool),
            typeof(InfoActionCardView),
            false,
            propertyChanged: OnStatusChanged);

    public bool IsStatusVisible
    {
        get => (bool)GetValue(IsStatusVisibleProperty);
        set => SetValue(IsStatusVisibleProperty, value);
    }


    private static void OnStatusChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoActionCardView control)
        {
            control.UpdateStatus();
            control.UpdateVisibility();
        }
    }

    private void UpdateStatus()
    {
        StatusLabel.Text = StatusText;
    }

    #endregion

    #region Action

    public static readonly BindableProperty ActionTextProperty =
        BindableProperty.Create(
            nameof(ActionText),
            typeof(string),
            typeof(InfoActionCardView),
            string.Empty,
            propertyChanged: OnActionChanged);

    public string ActionText
    {
        get => (string)GetValue(ActionTextProperty);
        set => SetValue(ActionTextProperty, value);
    }


    public static readonly BindableProperty ActionCommandProperty =
        BindableProperty.Create(
            nameof(ActionCommand),
            typeof(ICommand),
            typeof(InfoActionCardView),
            null,
            propertyChanged: OnActionChanged);

    public ICommand? ActionCommand
    {
        get => (ICommand?)GetValue(ActionCommandProperty);
        set => SetValue(ActionCommandProperty, value);
    }


    public static readonly BindableProperty ActionCommandParameterProperty =
        BindableProperty.Create(
            nameof(ActionCommandParameter),
            typeof(object),
            typeof(InfoActionCardView),
            null,
            propertyChanged: OnActionChanged);

    public object? ActionCommandParameter
    {
        get => GetValue(ActionCommandParameterProperty);
        set => SetValue(ActionCommandParameterProperty, value);
    }


    public static readonly BindableProperty IsActionVisibleProperty =
        BindableProperty.Create(
            nameof(IsActionVisible),
            typeof(bool),
            typeof(InfoActionCardView),
            false,
            propertyChanged: OnActionChanged);

    public bool IsActionVisible
    {
        get => (bool)GetValue(IsActionVisibleProperty);
        set => SetValue(IsActionVisibleProperty, value);
    }


    private static void OnActionChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoActionCardView control)
        {
            control.UpdateAction();
            control.UpdateVisibility();
        }
    }

    private void UpdateAction()
    {
        ActionButton.Text = ActionText;
        ActionButton.Command = ActionCommand;
        ActionButton.CommandParameter = ActionCommandParameter;
    }

    #endregion

    #region Colors
    public Color CardBackgroundColor
    {
        get => (Color)GetValue(CardBackgroundColorProperty);
        set => SetValue(CardBackgroundColorProperty, value);
    }
    public static readonly BindableProperty CardBackgroundColorProperty =
        BindableProperty.Create(
            nameof(CardBackgroundColor),
            typeof(Color),
            typeof(InfoActionCardView),
            Color.FromArgb("#FFF5F6"),
            propertyChanged: OnVisualPropertyChanged);

    public Color CardStrokeColor
    {
        get => (Color)GetValue(CardStrokeColorProperty);
        set => SetValue(CardStrokeColorProperty, value);
    }
    public static readonly BindableProperty CardStrokeColorProperty =
        BindableProperty.Create(
            nameof(CardStrokeColor),
            typeof(Color),
            typeof(InfoActionCardView),
            Color.FromArgb("#F4D5D9"),
            propertyChanged: OnVisualPropertyChanged);

    public Color TitleColor
    {
        get => (Color)GetValue(TitleColorProperty);
        set => SetValue(TitleColorProperty, value);
    }
    public static readonly BindableProperty TitleColorProperty =
        BindableProperty.Create(
            nameof(TitleColor),
            typeof(Color),
            typeof(InfoActionCardView),
            Color.FromArgb("#4a4a4a"),
            propertyChanged: OnVisualPropertyChanged);

    public Color DescriptionColor
    {
        get => (Color)GetValue(DescriptionColorProperty);
        set => SetValue(DescriptionColorProperty, value);
    }
    public static readonly BindableProperty DescriptionColorProperty =
        BindableProperty.Create(
            nameof(DescriptionColor),
            typeof(Color),
            typeof(InfoActionCardView),
            Color.FromArgb("#77717A"),
            propertyChanged: OnVisualPropertyChanged);

    public Color StatusBackgroundColor
    {
        get => (Color)GetValue(StatusBackgroundColorProperty);
        set => SetValue(StatusBackgroundColorProperty, value);
    }
    public static readonly BindableProperty StatusBackgroundColorProperty =
        BindableProperty.Create(
            nameof(StatusBackgroundColor),
            typeof(Color),
            typeof(InfoActionCardView),
            Color.FromArgb("#EAF4EC"),
            propertyChanged: OnVisualPropertyChanged);

    public Color StatusTextColor
    {
        get => (Color)GetValue(StatusTextColorProperty);
        set => SetValue(StatusTextColorProperty, value);
    }
    public static readonly BindableProperty StatusTextColorProperty =
        BindableProperty.Create(
            nameof(StatusTextColor),
            typeof(Color),
            typeof(InfoActionCardView),
            Color.FromArgb("#23865B"),
            propertyChanged: OnVisualPropertyChanged);

    public Color ActionTextColor
    {
        get => (Color)GetValue(ActionTextColorProperty);
        set => SetValue(ActionTextColorProperty, value);
    }
    public static readonly BindableProperty ActionTextColorProperty =
        BindableProperty.Create(
            nameof(ActionTextColor),
            typeof(Color),
            typeof(InfoActionCardView),
            Color.FromArgb("#D94B7D"),
            propertyChanged: OnVisualPropertyChanged);
    #endregion

    #region Update Methods

    private static void OnVisualPropertyChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoActionCardView control)
            control.UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        CardBorder.Background = CardBackgroundColor;
        CardBorder.Stroke = CardStrokeColor;

        TitleLabel.TextColor = TitleColor;
        DescriptionLabel.TextColor = DescriptionColor;

        StatusBorder.BackgroundColor = StatusBackgroundColor;
        StatusLabel.TextColor = StatusTextColor;

        ActionButton.TextColor = ActionTextColor;
    }

    private void UpdateVisibility()
    {
        StatusBorder.IsVisible =
            IsStatusVisible &&
            !string.IsNullOrWhiteSpace(StatusText);

        ActionButton.IsVisible =
            IsActionVisible &&
            !string.IsNullOrWhiteSpace(ActionText);
    }

    public string IconGlyph
    {
        get => (string)GetValue(IconGlyphProperty);
        set => SetValue(IconGlyphProperty, value);
    }
    public static readonly BindableProperty IconGlyphProperty =
            BindableProperty.Create(nameof(IconGlyph), typeof(string), typeof(InfoActionCardView), default(string), propertyChanged: OnIconGlyphChanged);

    private static void OnIconGlyphChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InfoActionCardView control)
            control.UpdateIconGlyph();
    }

    public bool IsIconVisibility
    {
        get => (bool)GetValue(IsIconVisibilityProperty);
        set => SetValue(IsIconVisibilityProperty, value);
    }
    public static readonly BindableProperty IsIconVisibilityProperty =
            BindableProperty.Create(nameof(IsIconVisibility), typeof(bool), typeof(InfoActionCardView), true, propertyChanged: OnIconGlyphChanged);

    public Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }
    public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create(
                nameof(IconColor), 
                typeof(Color), 
                typeof(InfoCardView), 
                Color.FromArgb("#474747"), propertyChanged:OnIconGlyphChanged);
    private void UpdateIconGlyph()
    {
        IconLable.Text = IconGlyph;
        IconLable.IsVisible = IsIconVisibility;
        IconLable.TextColor = IconColor;
    }
    #endregion
}