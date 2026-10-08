using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BeautySalonBooking.Maui.Components.Card;

[ContentProperty(nameof(Items))]
public partial class SectionCardView : ContentView
{
    public ObservableCollection<View> Items { get; } = new();

    private readonly Border _mainBorder;
    private readonly Border _headerBorder;
    private readonly Label _headerLabel;
    private readonly VerticalStackLayout _itemsLayout;

    public SectionCardView()
    {
        InitializeComponent();

        Items.CollectionChanged += OnItemsChanged;

        _headerLabel = new Label
        {
            FontAttributes = FontAttributes.Bold,
            FontSize = 13,
            TextColor = Color.FromArgb("#D94B7D"),
            VerticalTextAlignment = TextAlignment.Center
        };

        _headerBorder = new Border
        {
            Padding = new Thickness(10, 0),
            BackgroundColor = Color.FromArgb("#FFF0F6"),
            HeightRequest = 50,
            Content = _headerLabel
        };

        _itemsLayout = new VerticalStackLayout
        {
            Spacing = 0
        };

        var grid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = 50 },
                new RowDefinition { Height = GridLength.Auto }
            },
            RowSpacing = 0
        };

        grid.Add(_headerBorder);
        Grid.SetRow(_headerBorder, 0);

        grid.Add(_itemsLayout);
        Grid.SetRow(_itemsLayout, 1);

        _mainBorder = new Border
        {
            BackgroundColor = Colors.White,
            Stroke = Color.FromArgb("#F0E8ED"),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(12)
            },
            StrokeThickness = 1,
            Content = grid
        };

        Content = _mainBorder;

        UpdateHeader();
        UpdateItems();
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
            typeof(SectionCardView),
            string.Empty,
            propertyChanged: OnTitleChanged);

    private static void OnTitleChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SectionCardView control)
            control.UpdateHeader();
    }

    private void UpdateHeader()
    {
        if (_headerLabel is not null)
            _headerLabel.Text = Title;
    }
    #endregion

    #region ShowDividers

    public bool ShowDividers
    {
        get => (bool)GetValue(ShowDividersProperty);
        set => SetValue(ShowDividersProperty, value);
    }

    public static readonly BindableProperty ShowDividersProperty =
        BindableProperty.Create(
            nameof(ShowDividers),
            typeof(bool),
            typeof(SectionCardView),
            true,
            propertyChanged: OnAppearanceChanged);

    #endregion

    #region DividerColor

    public Color DividerColor
    {
        get => (Color)GetValue(DividerColorProperty);
        set => SetValue(DividerColorProperty, value);
    }

    public static readonly BindableProperty DividerColorProperty =
        BindableProperty.Create(
            nameof(DividerColor),
            typeof(Color),
            typeof(SectionCardView),
            Color.FromArgb("#F3EDF1"),
            propertyChanged: OnAppearanceChanged);

    #endregion

    #region DividerHeight

    public double DividerHeight
    {
        get => (double)GetValue(DividerHeightProperty);
        set => SetValue(DividerHeightProperty, value);
    }

    public static readonly BindableProperty DividerHeightProperty =
        BindableProperty.Create(
            nameof(DividerHeight),
            typeof(double),
            typeof(SectionCardView),
            1d,
            propertyChanged: OnAppearanceChanged);

    #endregion

    #region DividerMargin

    public Thickness DividerMargin
    {
        get => (Thickness)GetValue(DividerMarginProperty);
        set => SetValue(DividerMarginProperty, value);
    }

    public static readonly BindableProperty DividerMarginProperty =
        BindableProperty.Create(
            nameof(DividerMargin),
            typeof(Thickness),
            typeof(SectionCardView),
            new Thickness(16, 0),
            propertyChanged: OnAppearanceChanged);

    #endregion

    #region Collection

    private void OnItemsChanged(
        object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        UpdateItems();
    }

    private void UpdateItems()
    {
        if (_itemsLayout is null)
            return;

        _itemsLayout.Children.Clear();

        for (var index = 0; index < Items.Count; index++)
        {
            var item = Items[index];

            _itemsLayout.Children.Add(item);

            if (ShowDividers && index < Items.Count - 1)
            {
                _itemsLayout.Children.Add(CreateDivider());
            }
        }
    }

    private BoxView CreateDivider()
    {
        return new BoxView
        {
            HeightRequest = DividerHeight,
            Margin = DividerMargin,
            BackgroundColor = DividerColor
        };
    }

    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is SectionCardView control)
            control.UpdateItems();
    }

    #endregion
}