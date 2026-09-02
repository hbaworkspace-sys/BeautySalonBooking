using BeautySalonBooking.Maui.Components.DateAndTime.Models;
using System.Collections.Specialized;

namespace BeautySalonBooking.Maui.Components.DateAndTime;

public partial class TimeSelectionView : ContentView
{
    private bool _isUpdatingSelection;
    private INotifyCollectionChanged? _itemsSourceCollection;
    private readonly Dictionary<int, TimeItemView> _timeItemViews = new();

    public TimeSelectionView()
    {
        InitializeComponent();
    }

    #region ItemsSource

    public IEnumerable<TimeSelectionItem>? ItemsSource
    {
        get => (IEnumerable<TimeSelectionItem>?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable<TimeSelectionItem>),
            typeof(TimeSelectionView),
            null,
            propertyChanged: OnItemsSourceChanged);

    #endregion

    #region SelectedItem

    public TimeSelectionItem? SelectedTime
    {
        get => (TimeSelectionItem?)GetValue(SelectedTimeProperty);
        set => SetValue(SelectedTimeProperty, value);
    }

    public static readonly BindableProperty SelectedTimeProperty =
        BindableProperty.Create(
            nameof(SelectedTime),
            typeof(TimeSelectionItem),
            typeof(TimeSelectionView),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedTimeChanged);

    private static void OnSelectedTimeChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (TimeSelectionView)bindable;

        if (control._isUpdatingSelection)
            return;

        if (newValue is not TimeSelectionItem selectedItem)
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
            typeof(TimeSelectionView),
            -1,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedIndexChanged);

    private static void OnSelectedIndexChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (TimeSelectionView)bindable;

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
            typeof(TimeSelectionView),
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
            typeof(TimeSelectionView),
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
            typeof(TimeSelectionView),
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
            typeof(TimeSelectionView),
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
            typeof(TimeSelectionView),
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
            typeof(TimeSelectionView),
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
            typeof(TimeSelectionView),
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
            typeof(TimeSelectionView),
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
            typeof(TimeSelectionView),
            12f,
            propertyChanged: OnAppearanceChanged);

    #endregion

    #region Events

    public event EventHandler<TimeSelectionChangedEventArgs>? SelectionChanged;

    #endregion

    #region Property Changed

    private static void OnItemsSourceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (TimeSelectionView)bindable;

        control.UnsubscribeFromItemsSource(oldValue);
        control.SubscribeToItemsSource(newValue);

        control.UpdateItems();
    }
    private void SubscribeToItemsSource(object? source)
    {
        if (source is not INotifyCollectionChanged collection)
            return;

        _itemsSourceCollection = collection;

        collection.CollectionChanged += OnItemsSourceCollectionChanged;
    }

    private void UnsubscribeFromItemsSource(object? source)
    {
        if (_itemsSourceCollection is null)
            return;

        _itemsSourceCollection.CollectionChanged -=
            OnItemsSourceCollectionChanged;

        _itemsSourceCollection = null;
    }

    private void OnItemsSourceCollectionChanged(
        object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(UpdateItems);
    }

    private static void OnAppearanceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (TimeSelectionView)bindable;

        control.UpdateVisibleItems();
    }

    #endregion

    #region Items

    private void UpdateItems()
    {
        TimesLayout.Clear();
        TimesLayout.RowDefinitions.Clear();
        _timeItemViews.Clear();

        if (ItemsSource is null)
        {
            SelectedTime = null;
            SelectedIndex = -1;
            return;
        }

        var items = ItemsSource.ToList();

        if (items.Count == 0)
        {
            SelectedTime = null;
            SelectedIndex = -1;
            return;
        }

        var rowCount = (int)Math.Ceiling(items.Count / 3d);

        for (var row = 0; row < rowCount; row++)
        {
            TimesLayout.RowDefinitions.Add(
                new RowDefinition
                {
                    Height = GridLength.Auto
                });
        }

        for (var index = 0; index < items.Count; index++)
        {
            var item = items[index];

            var row = index / 3;
            var column = index % 3;

            var timeItem = CreateTimeItem(item);

            Grid.SetRow(timeItem, row);
            Grid.SetColumn(timeItem, column);

            _timeItemViews[item.Id] = timeItem;

            TimesLayout.Children.Add(timeItem);
        }

        var selectedItem =
            items.FirstOrDefault(x => x.IsSelected);

        if (selectedItem is null)
        {
            SelectedTime = null;
            SelectedIndex = -1;
            return;
        }

        _isUpdatingSelection = true;

        try
        {
            SelectedTime = selectedItem;

            SelectedIndex =
                items.FindIndex(x => x.Id == selectedItem.Id);
        }
        finally
        {
            _isUpdatingSelection = false;
        }
    }

    private TimeItemView CreateTimeItem(TimeSelectionItem item)
    {
        var timeItem = new TimeItemView
        {
            Time = item.DisplayTime,
            IsSelected = item.IsSelected
        };

        ApplyAppearance(timeItem);

        var tapGesture = new TapGestureRecognizer();

        tapGesture.Tapped += (_, _) =>
        {
            SelectItem(item);
        };

        timeItem.GestureRecognizers.Add(tapGesture);

        return timeItem;
    }

    #endregion

    #region Appearance

    private void ApplyAppearance(TimeItemView timeItem)
    {
        timeItem.NormalBackground = NormalBackground;
        timeItem.SelectedBackground = SelectedBackground;

        timeItem.NormalTextColor = NormalTextColor;
        timeItem.SelectedTextColor = SelectedTextColor;

        timeItem.BorderBrush = BorderBrush;
        timeItem.BorderWidth = BorderWidth;
        timeItem.SelectedBorderWidth = SelectedBorderWidth;
        timeItem.CornerRadius = CornerRadius;
    }


    private void UpdateVisibleItems()
    {
        foreach (var child in TimesLayout.Children)
        {
            if (child is TimeItemView timeItem)
                ApplyAppearance(timeItem);
        }
    }

    #endregion

    #region Selection

    private void SelectItem(TimeSelectionItem selectedItem)
    {
        if (!IsSelectionEnabled)
            return;

        if (selectedItem.IsSelected)
            return;

        var previousItem = ItemsSource?
            .FirstOrDefault(x => x.IsSelected);

        var previousTime = previousItem?.Time;

        // Unselect previous item
        if (previousItem is not null)
        {
            previousItem.IsSelected = false;

            if (_timeItemViews.TryGetValue(
                    previousItem.Id,
                    out var previousView))
            {
                previousView.IsSelected = false;
            }
        }

        // Select new item
        selectedItem.IsSelected = true;

        if (_timeItemViews.TryGetValue(
                selectedItem.Id,
                out var selectedView))
        {
            selectedView.IsSelected = true;
        }

        _isUpdatingSelection = true;

        try
        {
            SelectedTime = selectedItem;

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
            new TimeSelectionChangedEventArgs(
                previousTime,
                selectedItem.Time));
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


    private void SelectItemWithoutEvent(TimeSelectionItem selectedItem)
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

                if (_timeItemViews.TryGetValue(
                        previousItem.Id,
                        out var previousView))
                {
                    previousView.IsSelected = false;
                }
            }

            // Select new
            selectedItem.IsSelected = true;

            if (_timeItemViews.TryGetValue(
                    selectedItem.Id,
                    out var selectedView))
            {
                selectedView.IsSelected = true;
            }

            SelectedTime = selectedItem;

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
}