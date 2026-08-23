using BeautySalonBooking.Maui.Components.DateAndTime.Models;

namespace BeautySalonBooking.Maui.Components.DateAndTime;

public partial class DateSelectionView : ContentView
{
    private bool _isUpdatingSelection;

    private readonly Dictionary<int, DateItemView> _dateItemViews = new();

    public DateSelectionView()
    {
        InitializeComponent();
    }

    #region ItemsSource

    public IEnumerable<DateSelectionItem>? ItemsSource
    {
        get => (IEnumerable<DateSelectionItem>?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<DateSelectionItem>),
            typeof(DateSelectionView),
            null,
            propertyChanged: OnItemsSourceChanged);

    #endregion

    #region SelectedItem
    public DateSelectionItem? SelectedDate
    {
        get => (DateSelectionItem?)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    public static readonly BindableProperty SelectedDateProperty =
        BindableProperty.Create(
            nameof(SelectedDate),
            typeof(DateSelectionItem),
            typeof(DateSelectionView),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedDateChanged);

    private static void OnSelectedDateChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (DateSelectionView)bindable;

        if (control._isUpdatingSelection)
            return;

        if (newValue is not DateSelectionItem selectedItem)
            return;

        control.SelectItemWithoutEvent(selectedItem);
    }

    #endregion

    #region SelectedIndex

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    public static readonly BindableProperty SelectedIndexProperty =
        BindableProperty.Create(
            nameof(SelectedIndex),
            typeof(int),
            typeof(DateSelectionView),
            -1,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedIndexChanged);

    private static void OnSelectedIndexChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (DateSelectionView)bindable;

        if (control._isUpdatingSelection)
            return;

        control.UpdateSelectionFromIndex();
    }

    #endregion

    #region Selection

    public bool IsSelectionEnabled
    {
        get => (bool)GetValue(IsSelectionEnabledProperty);
        set => SetValue(IsSelectionEnabledProperty, value);
    }

    public static readonly BindableProperty IsSelectionEnabledProperty =
        BindableProperty.Create(
            nameof(IsSelectionEnabled),
            typeof(bool),
            typeof(DateSelectionView),
            true);

    #endregion

    #region Appearance

    public Brush NormalBackground
    {
        get => (Brush)GetValue(NormalBackgroundProperty);
        set => SetValue(NormalBackgroundProperty, value);
    }

    public static readonly BindableProperty NormalBackgroundProperty =
        BindableProperty.Create(
            nameof(NormalBackground),
            typeof(Brush),
            typeof(DateSelectionView),
            new SolidColorBrush(Color.FromArgb("#FFFFFF")),
            propertyChanged: OnAppearanceChanged);


    public Brush SelectedBackground
    {
        get => (Brush)GetValue(SelectedBackgroundProperty);
        set => SetValue(SelectedBackgroundProperty, value);
    }

    public static readonly BindableProperty SelectedBackgroundProperty =
        BindableProperty.Create(
            nameof(SelectedBackground),
            typeof(Brush),
            typeof(DateSelectionView),
            new SolidColorBrush(Color.FromArgb("#F05C80")),
            propertyChanged: OnAppearanceChanged);


    public Color NormalTextColor
    {
        get => (Color)GetValue(NormalTextColorProperty);
        set => SetValue(NormalTextColorProperty, value);
    }

    public static readonly BindableProperty NormalTextColorProperty =
        BindableProperty.Create(
            nameof(NormalTextColor),
            typeof(Color),
            typeof(DateSelectionView),
            Color.FromArgb("#6F666B"),
            propertyChanged: OnAppearanceChanged);


    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    public static readonly BindableProperty SelectedTextColorProperty =
        BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(DateSelectionView),
            Colors.White,
            propertyChanged: OnAppearanceChanged);


    public Brush BorderBrush
    {
        get => (Brush)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(DateSelectionView),
            new SolidColorBrush(Color.FromArgb("#E8E1E5")),
            propertyChanged: OnAppearanceChanged);


    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(DateSelectionView),
            1d,
            propertyChanged: OnAppearanceChanged);


    public double SelectedBorderWidth
    {
        get => (double)GetValue(SelectedBorderWidthProperty);
        set => SetValue(SelectedBorderWidthProperty, value);
    }

    public static readonly BindableProperty SelectedBorderWidthProperty =
        BindableProperty.Create(
            nameof(SelectedBorderWidth),
            typeof(double),
            typeof(DateSelectionView),
            0d,
            propertyChanged: OnAppearanceChanged);


    public float CornerRadius
    {
        get => (float)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(float),
            typeof(DateSelectionView),
            12f,
            propertyChanged: OnAppearanceChanged);

    #endregion

    #region Events

    public event EventHandler<DateSelectionChangedEventArgs>? SelectionChanged;

    #endregion

    #region Property Changed

    private static void OnItemsSourceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (DateSelectionView)bindable;

        control.UpdateItems();
    }


    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (DateSelectionView)bindable;

        control.UpdateVisibleItems();
    }

    #endregion

    #region Items

    private void UpdateItems()
    {
        DatesLayout.Clear();
        _dateItemViews.Clear();

        if (ItemsSource is null)
            return;

        foreach (var item in ItemsSource)
        {
            var dateItem = CreateDateItem(item);

            _dateItemViews[item.Id] = dateItem;

            DatesLayout.Children.Add(dateItem);
        }
    }


    private DateItemView CreateDateItem(DateSelectionItem item)
    {
        var dateItem = new DateItemView
        {
            DayName = item.DayName,
            DayNumber = item.DayNumber,
            MonthName = item.MonthName,
            IsSelected = item.IsSelected
        };

        ApplyAppearance(dateItem);

        var tapGesture = new TapGestureRecognizer();

        tapGesture.Tapped += (_, _) =>
        {
            SelectItem(item);
        };

        dateItem.GestureRecognizers.Add(tapGesture);

        return dateItem;
    }

    #endregion

    #region Appearance

    private void ApplyAppearance(DateItemView dateItem)
    {
        dateItem.NormalBackground = NormalBackground;
        dateItem.SelectedBackground = SelectedBackground;

        dateItem.NormalTextColor = NormalTextColor;
        dateItem.SelectedTextColor = SelectedTextColor;

        dateItem.BorderBrush = BorderBrush;
        dateItem.BorderWidth = BorderWidth;
        dateItem.SelectedBorderWidth = SelectedBorderWidth;
        dateItem.CornerRadius = CornerRadius;
    }


    private void UpdateVisibleItems()
    {
        foreach (var child in DatesLayout.Children)
        {
            if (child is DateItemView dateItem)
                ApplyAppearance(dateItem);
        }
    }

    #endregion

    #region Selection

    private void SelectItem(DateSelectionItem selectedItem)
    {
        if (!IsSelectionEnabled)
            return;

        if (selectedItem.IsSelected)
            return;

        var previousItem = ItemsSource?
            .FirstOrDefault(x => x.IsSelected);

        var previousDate = previousItem?.Date;

        // Unselect previous item
        if (previousItem is not null)
        {
            previousItem.IsSelected = false;

            if (_dateItemViews.TryGetValue(
                    previousItem.Id,
                    out var previousView))
            {
                previousView.IsSelected = false;
            }
        }

        // Select new item
        selectedItem.IsSelected = true;

        if (_dateItemViews.TryGetValue(
                selectedItem.Id,
                out var selectedView))
        {
            selectedView.IsSelected = true;
        }

        _isUpdatingSelection = true;

        try
        {
            SelectedDate = selectedItem;

            SelectedIndex = ItemsSource?
                .ToList()
                .FindIndex(x => x.Id == selectedItem.Id) ?? -1;
        }
        finally
        {
            _isUpdatingSelection = false;
        }

        SelectionChanged?.Invoke(
            this,
            new DateSelectionChangedEventArgs(
                previousDate,
                selectedItem.Date));
    }


    private void UpdateSelectionFromIndex()
    {
        if (ItemsSource is null)
            return;

        var items = ItemsSource.ToList();

        if (SelectedIndex < 0 || SelectedIndex >= items.Count)
            return;

        SelectItemWithoutEvent(items[SelectedIndex]);
    }


    private void SelectItemWithoutEvent(DateSelectionItem selectedItem)
    {
        _isUpdatingSelection = true;

        try
        {
            var previousItem = ItemsSource?
                .FirstOrDefault(x => x.IsSelected);

            // Unselect previous
            if (previousItem is not null &&
                previousItem.Id != selectedItem.Id)
            {
                previousItem.IsSelected = false;

                if (_dateItemViews.TryGetValue(
                        previousItem.Id,
                        out var previousView))
                {
                    previousView.IsSelected = false;
                }
            }

            // Select new
            selectedItem.IsSelected = true;

            if (_dateItemViews.TryGetValue(
                    selectedItem.Id,
                    out var selectedView))
            {
                selectedView.IsSelected = true;
            }

            SelectedDate = selectedItem;

            SelectedIndex = ItemsSource?
                .ToList()
                .FindIndex(
                    x => x.Id == selectedItem.Id) ?? -1;
        }
        finally
        {
            _isUpdatingSelection = false;
        }
    }

    #endregion

    #region Public Methods

    public void ScrollToSelected(
        ScrollToPosition position = ScrollToPosition.Center,
        bool animate = true)
    {
        // TODO:
        // ScrollView + HorizontalStackLayout
        // scrolling will be implemented separately.
    }

    #endregion
}