using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

namespace BeautySalonBooking.Maui.Components.Input;

public partial class ComboBox : ContentView
{
    private readonly ObservableCollection<object> _filteredItems = new();

    private bool _isUpdatingItems;
    private bool _isSelectingItem;

    public event EventHandler? SelectionChanged;

    public ComboBox()
    {
        InitializeComponent();
        InitializeControl();
    }

    private void InitializeControl()
    {
        UpdatePlaceholder();
        UpdateSelectedItem();
        UpdateSearch();
        UpdateArrow();
        UpdateItemsSource();
        UpdateItemTemplate();
    }

    #region Items

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(ComboBox),
            null,
            propertyChanged: OnItemsSourceChanged);

    private static void OnItemsSourceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not ComboBox control)
            return;

        control.UpdateItemsSource();
    }

    private void UpdateItemsSource()
    {
        if (_isUpdatingItems)
            return;

        _isUpdatingItems = true;

        try
        {
            _filteredItems.Clear();

            if (ItemsSource is not null)
            {
                foreach (var item in ItemsSource)
                {
                    if (item is not null)
                        _filteredItems.Add(item);
                }
            }

            ItemsCollectionView.ItemsSource = _filteredItems;
        }
        finally
        {
            _isUpdatingItems = false;
        }

        UpdateItemTemplate();
    }

    #endregion

    #region Selected Item

    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(ComboBox),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedItemChanged);

    private static void OnSelectedItemChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not ComboBox control)
            return;

        control.UpdateSelectedItem();

        if (!control._isSelectingItem)
        {
            control.ItemsCollectionView.SelectedItem = newValue;
        }

        control.SelectionChanged?.Invoke(
            control,
            EventArgs.Empty);
    }

    private void UpdateSelectedItem()
    {
        if (SelectedItem is null)
        {
            SelectedItemLabel.Text = Placeholder;
            SelectedItemLabel.TextColor = PlaceholderColor;
            return;
        }

        SelectedItemLabel.Text = GetDisplayText(SelectedItem);
        SelectedItemLabel.TextColor = SelectedTextColor;
    }

    #endregion

    #region Display

    public string? DisplayMember
    {
        get => (string?)GetValue(DisplayMemberProperty);
        set => SetValue(DisplayMemberProperty, value);
    }

    public static readonly BindableProperty DisplayMemberProperty =
        BindableProperty.Create(
            nameof(DisplayMember),
            typeof(string),
            typeof(ComboBox),
            null,
            propertyChanged: OnDisplayMemberChanged);

    private static void OnDisplayMemberChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not ComboBox control)
            return;

        control.UpdateSelectedItem();
        control.UpdateItemTemplate();
    }

    private string GetDisplayText(object item)
    {
        if (string.IsNullOrWhiteSpace(DisplayMember))
            return item.ToString() ?? string.Empty;

        var property = item.GetType().GetProperty(
            DisplayMember,
            BindingFlags.Public | BindingFlags.Instance);

        if (property is null)
            return item.ToString() ?? string.Empty;

        return property.GetValue(item)?.ToString() ?? string.Empty;
    }

    private void UpdateItemTemplate()
    {
        ItemsCollectionView.ItemTemplate = new DataTemplate(() =>
        {
            var label = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            label.SetBinding(
                Label.TextProperty,
                new Binding(".",
                    converter: new DisplayMemberConverter(
                        () => DisplayMember)));

            var grid = new Grid
            {
                HeightRequest = 45,
                Padding = new Thickness(12)
            };

            grid.Children.Add(label);

            return grid;
        });
    }

    #endregion

    #region Placeholder

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(ComboBox),
            "انتخاب کنید",
            propertyChanged: OnPlaceholderChanged);

    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    public static readonly BindableProperty PlaceholderColorProperty =
        BindableProperty.Create(
            nameof(PlaceholderColor),
            typeof(Color),
            typeof(ComboBox),
            Colors.Gray,
            propertyChanged: OnPlaceholderChanged);

    private static void OnPlaceholderChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not ComboBox control)
            return;

        control.UpdatePlaceholder();
        control.UpdateSelectedItem();
    }

    private void UpdatePlaceholder()
    {
        if (SelectedItem is null)
        {
            SelectedItemLabel.Text = Placeholder;
            SelectedItemLabel.TextColor = PlaceholderColor;
        }
    }

    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    public static readonly BindableProperty SelectedTextColorProperty =
        BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(ComboBox),
            Colors.Black);

    #endregion

    #region Search

    public bool IsSearchable
    {
        get => (bool)GetValue(IsSearchableProperty);
        set => SetValue(IsSearchableProperty, value);
    }

    public static readonly BindableProperty IsSearchableProperty =
        BindableProperty.Create(
            nameof(IsSearchable),
            typeof(bool),
            typeof(ComboBox),
            true,
            propertyChanged: OnSearchChanged);

    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    public static readonly BindableProperty SearchTextProperty =
        BindableProperty.Create(
            nameof(SearchText),
            typeof(string),
            typeof(ComboBox),
            string.Empty,
            propertyChanged: OnSearchTextChanged);

    private static void OnSearchChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not ComboBox control)
            return;

        control.UpdateSearch();
    }

    private static void OnSearchTextChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not ComboBox control)
            return;

        control.FilterItems(newValue?.ToString() ?? string.Empty);
    }

    private void UpdateSearch()
    {
        SearchBorder.IsVisible = IsSearchable;

        if (!IsSearchable)
            SearchEntry.Text = string.Empty;
    }

    private void SearchEntry_TextChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        SearchText = e.NewTextValue ?? string.Empty;
    }

    private void FilterItems(string searchText)
    {
        if (_isUpdatingItems)
            return;

        _isUpdatingItems = true;

        try
        {
            _filteredItems.Clear();

            if (ItemsSource is not null)
            {
                foreach (var item in ItemsSource)
                {
                    if (item is null)
                        continue;

                    if (string.IsNullOrWhiteSpace(searchText) ||
                        GetDisplayText(item)
                            .Contains(
                                searchText,
                                StringComparison.OrdinalIgnoreCase))
                    {
                        _filteredItems.Add(item);
                    }
                }
            }
        }
        finally
        {
            _isUpdatingItems = false;
        }
    }

    #endregion

    #region Open / Close

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        private set => SetValue(IsOpenPropertyKey, value);
    }

    private static readonly BindablePropertyKey IsOpenPropertyKey =
        BindableProperty.CreateReadOnly(
            nameof(IsOpen),
            typeof(bool),
            typeof(ComboBox),
            false);

    public static readonly BindableProperty IsOpenProperty =
        IsOpenPropertyKey.BindableProperty;

    private void MainBorder_Tapped(
        object? sender,
        TappedEventArgs e)
    {
        if (!IsEnabled)
            return;

        if (IsOpen)
            CloseDropdown();
        else
            OpenDropdown();
    }

    private void OpenDropdown()
    {
        IsOpen = true;

        DropdownBorder.IsVisible = true;
        ArrowLabel.Text = "▲";

        if (IsSearchable)
        {
            SearchEntry.Text = string.Empty;
            SearchEntry.Focus();
        }
    }

    private void CloseDropdown()
    {
        IsOpen = false;

        DropdownBorder.IsVisible = false;
        ArrowLabel.Text = "▼";

        SearchEntry.Unfocus();
    }

    #endregion

    #region Selection

    private void ItemsCollectionView_SelectionChanged(
        object? sender,
        SelectionChangedEventArgs e)
    {
        if (_isSelectingItem)
            return;

        var selectedItem = e.CurrentSelection.FirstOrDefault();

        if (selectedItem is null)
            return;

        _isSelectingItem = true;

        try
        {
            SelectedItem = selectedItem;
            SearchEntry.Text = string.Empty;
            CloseDropdown();
        }
        finally
        {
            _isSelectingItem = false;
        }
    }

    #endregion

    #region Arrow

    private void UpdateArrow()
    {
        ArrowLabel.Text = "▼";
    }

    #endregion
}

internal sealed class DisplayMemberConverter : IValueConverter
{
    private readonly Func<string?> _displayMemberProvider;

    public DisplayMemberConverter(Func<string?> displayMemberProvider)
    {
        _displayMemberProvider = displayMemberProvider;
    }

    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        System.Globalization.CultureInfo culture)
    {
        if (value is null)
            return string.Empty;

        var displayMember = _displayMemberProvider();

        if (string.IsNullOrWhiteSpace(displayMember))
            return value.ToString() ?? string.Empty;

        var property = value.GetType().GetProperty(displayMember);

        return property?.GetValue(value)?.ToString()
               ?? value.ToString()
               ?? string.Empty;
    }

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        System.Globalization.CultureInfo culture)
    {
        return BindableProperty.UnsetValue;
    }
}