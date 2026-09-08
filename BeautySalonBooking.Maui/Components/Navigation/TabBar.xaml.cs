using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace BeautySalonBooking.Maui.Components.Navigation;

public partial class TabBar : ContentView
{
    #region Fields

    private bool _isUpdatingSelection;

    #endregion


    #region Constructor

    public TabBar()
    {
        InitializeComponent();

        Items.CollectionChanged += OnItemsCollectionChanged;

        UpdateLayout();
        UpdateAppearance();
        UpdateSelection();
    }

    #endregion


    #region Items
    public ObservableCollection<TabItemView> Items { get; } = new();
    #endregion


    #region SelectedIndex

    public static readonly BindableProperty SelectedIndexProperty =
        BindableProperty.Create(
            nameof(SelectedIndex),
            typeof(int),
            typeof(TabBar),
            -1,
            propertyChanged: OnSelectedIndexChanged);

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }


    private static void OnSelectedIndexChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var tabBar = (TabBar)bindable;

        if (tabBar._isUpdatingSelection)
            return;

        var oldIndex = (int)oldValue;
        var newIndex = (int)newValue;

        if (newIndex < -1)
        {
            tabBar.SetSelectedIndex(
                -1,
                raiseEvent: true);

            return;
        }

        if (newIndex >= tabBar.Items.Count)
        {
            return;
        }

        tabBar.ApplySelection(
            oldIndex,
            newIndex,
            raiseEvent: true);
    }

    #endregion


    #region SelectedItem
    public TabItemView? SelectedItem
    {
        get => (TabItemView?)GetValue(SelectedItemProperty);
        private set => SetValue(SelectedItemProperty, value);
    }
    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(
            nameof(SelectedItem),
            typeof(TabItemView),
            typeof(TabBar),
            null);
    #endregion


    #region Placement
    public static readonly BindableProperty PlacementProperty =
        BindableProperty.Create(
            nameof(Placement),
            typeof(TabBarPlacement),
            typeof(TabBar),
            TabBarPlacement.Bottom,
            propertyChanged: OnPlacementChanged);


    public TabBarPlacement Placement
    {
        get => (TabBarPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }


    private static void OnPlacementChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var tabBar = (TabBar)bindable;

        tabBar.UpdateLayout();
    }

    #endregion


    #region Layout

    public static readonly BindableProperty ItemSpacingProperty =
        BindableProperty.Create(
            nameof(ItemSpacing),
            typeof(double),
            typeof(TabBar),
            0d,
            propertyChanged: OnLayoutPropertyChanged);


    public double ItemSpacing
    {
        get => (double)GetValue(ItemSpacingProperty);
        set => SetValue(ItemSpacingProperty, value);
    }


    public static readonly BindableProperty ContentPaddingProperty =
        BindableProperty.Create(
            nameof(ContentPadding),
            typeof(Thickness),
            typeof(TabBar),
            new Thickness(0),
            propertyChanged: OnLayoutPropertyChanged);


    public Thickness ContentPadding
    {
        get => (Thickness)GetValue(ContentPaddingProperty);
        set => SetValue(ContentPaddingProperty, value);
    }


    public static readonly BindableProperty ItemsHorizontalOptionsProperty =
        BindableProperty.Create(
            nameof(ItemsHorizontalOptions),
            typeof(LayoutOptions),
            typeof(TabBar),
            LayoutOptions.Fill,
            propertyChanged: OnLayoutPropertyChanged);


    public LayoutOptions ItemsHorizontalOptions
    {
        get => (LayoutOptions)GetValue(ItemsHorizontalOptionsProperty);
        set => SetValue(ItemsHorizontalOptionsProperty, value);
    }


    public static readonly BindableProperty ItemsVerticalOptionsProperty =
        BindableProperty.Create(
            nameof(ItemsVerticalOptions),
            typeof(LayoutOptions),
            typeof(TabBar),
            LayoutOptions.Fill,
            propertyChanged: OnLayoutPropertyChanged);


    public LayoutOptions ItemsVerticalOptions
    {
        get => (LayoutOptions)GetValue(ItemsVerticalOptionsProperty);
        set => SetValue(ItemsVerticalOptionsProperty, value);
    }


    private static void OnLayoutPropertyChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var tabBar = (TabBar)bindable;

        tabBar.UpdateLayout();
    }

    #endregion


    #region Appearance

    public static readonly BindableProperty FillBrushProperty =
        BindableProperty.Create(
            nameof(FillBrush),
            typeof(Brush),
            typeof(TabBar),
            Brush.Transparent,
            propertyChanged: OnAppearancePropertyChanged);

    public Brush? FillBrush
    {
        get => (Brush?)GetValue(FillBrushProperty);
        set => SetValue(FillBrushProperty, value);
    }


    public static readonly BindableProperty BorderBrushProperty =
        BindableProperty.Create(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(TabBar),
            Brush.Transparent,
            propertyChanged: OnAppearancePropertyChanged);


    public Brush? BorderBrush
    {
        get => (Brush?)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }
    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(
            nameof(BorderWidth),
            typeof(double),
            typeof(TabBar),
            0d,
            propertyChanged: OnAppearancePropertyChanged);


    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(TabBar),
            new CornerRadius(12),
            propertyChanged: OnAppearancePropertyChanged);

    public static readonly BindableProperty BarShadowProperty =
        BindableProperty.Create(
            nameof(BarShadow),
            typeof(Shadow),
            typeof(TabBar),
            default(Shadow),
            propertyChanged: OnAppearancePropertyChanged);

    public Shadow? BarShadow
    {
        get => (Shadow?)GetValue(BarShadowProperty);
        set => SetValue(BarShadowProperty, value);
    }
    private static void OnAppearancePropertyChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var tabBar = (TabBar)bindable;

        tabBar.UpdateAppearance();
    }

    #endregion


    #region Selection Command

    public static readonly BindableProperty SelectionChangedCommandProperty =
        BindableProperty.Create(
            nameof(SelectionChangedCommand),
            typeof(ICommand),
            typeof(TabBar));
    public ICommand? SelectionChangedCommand
    {
        get => (ICommand?)GetValue(SelectionChangedCommandProperty);
        set => SetValue(SelectionChangedCommandProperty, value);
    }


    public static readonly BindableProperty SelectionChangedCommandParameterProperty =
        BindableProperty.Create(
            nameof(SelectionChangedCommandParameter),
            typeof(object),
            typeof(TabBar));
    public object? SelectionChangedCommandParameter
    {
        get => GetValue(SelectionChangedCommandParameterProperty);
        set => SetValue(SelectionChangedCommandParameterProperty, value);
    }

    #endregion


    #region Events

    public event EventHandler<TabBarSelectionChangedEventArgs>? SelectionChanged;

    #endregion


    #region Collection Handling

    private void OnItemsCollectionChanged(
        object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        UpdateLayout();

        if (Items.Count == 0)
        {
            SetSelectedIndex(
                -1,
                raiseEvent: true);

            return;
        }

        if (SelectedIndex == -1)
        {
            UpdateSelection();

            return;
        }

        if (SelectedIndex < Items.Count)
        {
            UpdateSelection();
        }
    }

    #endregion

    #region Tab Events
    private void OnTabTapped(
        object? sender,
        TappedEventArgs e)
    {
        if (sender is not TabItemView tab)
            return;

        var index = Items.IndexOf(tab);

        if (index < 0)
            return;

        Select(index);
    }
    #endregion

    #region Selection
    public void Select(int index)
    {
        if (index < 0 ||
            index >= Items.Count)
        {
            return;
        }

        if (SelectedIndex == index)
            return;

        SetSelectedIndex(
            index,
            raiseEvent: true);
    }


    public void Select(TabItemView tab)
    {
        if (tab is null)
            return;

        var index = Items.IndexOf(tab);

        if (index < 0)
            return;

        Select(index);
    }

    public void ClearSelection()
    {
        if (SelectedIndex == -1)
            return;

        SetSelectedIndex(
            -1,
            raiseEvent: true);
    }

    private void SetSelectedIndex(
        int index,
        bool raiseEvent)
    {
        if (index < -1)
            index = -1;

        if (index >= Items.Count)
            return;

        var oldIndex = SelectedIndex;

        if (oldIndex == index)
        {
            UpdateSelection();
            return;
        }

        _isUpdatingSelection = true;

        try
        {
            SelectedIndex = index;
        }
        finally
        {
            _isUpdatingSelection = false;
        }

        ApplySelection(
            oldIndex,
            index,
            raiseEvent);
    }


    private void ApplySelection(
        int oldIndex,
        int newIndex,
        bool raiseEvent)
    {
        if (newIndex < -1 ||
            newIndex >= Items.Count)
        {
            return;
        }

        var oldItem =
            oldIndex >= 0 &&
            oldIndex < Items.Count
                ? Items[oldIndex]
                : null;

        var newItem =
            newIndex >= 0 &&
            newIndex < Items.Count
                ? Items[newIndex]
                : null;

        foreach (var item in Items)
        {
            item.IsSelected =
                ReferenceEquals(item, newItem);
        }

        SelectedItem = newItem;

        if (!raiseEvent)
            return;

        RaiseSelectionChanged(
            oldIndex,
            newIndex,
            oldItem,
            newItem);
    }

    private void UpdateSelection()
    {
        var selectedItem =
            SelectedIndex >= 0 &&
            SelectedIndex < Items.Count
                ? Items[SelectedIndex]
                : null;

        foreach (var item in Items)
        {
            item.IsSelected =
                ReferenceEquals(item, selectedItem);
        }

        SelectedItem = selectedItem;
    }


    private void RaiseSelectionChanged(
        int oldIndex,
        int newIndex,
        TabItemView? oldItem,
        TabItemView? newItem)
    {
        var args = new TabBarSelectionChangedEventArgs(
            oldIndex,
            newIndex,
            oldItem,
            newItem);

        SelectionChanged?.Invoke(
            this,
            args);

        if (SelectionChangedCommand?.CanExecute(
                SelectionChangedCommandParameter) == true)
        {
            SelectionChangedCommand.Execute(
                SelectionChangedCommandParameter);
        }
    }

    #endregion


    #region Layout

    private void UpdateLayout()
    {
        if (TabContainer is null)
            return;

        /*
         * ابتدا eventهای قبلی را حذف می‌کنیم
         * تا یک Tab چند بار به TabBar متصل نشود.
         */
        foreach (var item in Items)
        {
            item.Tapped -= OnTabTapped;
        }

        TabContainer.Children.Clear();

        TabContainer.RowDefinitions.Clear();
        TabContainer.ColumnDefinitions.Clear();

        TabContainer.RowSpacing = 0;
        TabContainer.ColumnSpacing = ItemSpacing;

        TabContainer.Padding = ContentPadding;

        TabContainer.HorizontalOptions =
            ItemsHorizontalOptions;

        TabContainer.VerticalOptions =
            ItemsVerticalOptions;


        /*
         * Top / Bottom
         *
         * Tabها در یک ردیف کنار یکدیگر قرار می‌گیرند.
         */
        if (Placement == TabBarPlacement.Top ||
            Placement == TabBarPlacement.Bottom)
        {
            TabContainer.RowDefinitions.Add(
                new RowDefinition(
                    GridLength.Star));

            for (var index = 0;
                 index < Items.Count;
                 index++)
            {
                TabContainer.ColumnDefinitions.Add(
                    new ColumnDefinition(
                        GridLength.Star));

                var item = Items[index];

                Grid.SetRow(item, 0);
                Grid.SetColumn(item, index);

                item.Tapped += OnTabTapped;

                TabContainer.Children.Add(item);
            }
        }

        /*
         * Left / Right
         *
         * Tabها در یک ستون زیر یکدیگر قرار می‌گیرند.
         */
        else
        {
            TabContainer.ColumnDefinitions.Add(
                new ColumnDefinition(
                    GridLength.Star));

            for (var index = 0;
                 index < Items.Count;
                 index++)
            {
                TabContainer.RowDefinitions.Add(
                    new RowDefinition(
                        GridLength.Star));

                var item = Items[index];

                Grid.SetRow(item, index);
                Grid.SetColumn(item, 0);

                item.Tapped += OnTabTapped;

                TabContainer.Children.Add(item);
            }
        }

        UpdateSelection();
    }

    #endregion


    #region Appearance Update

    private void UpdateAppearance()
    {
        if (ContainerBorder is null)
            return;

        ContainerBorder.Background =
            FillBrush ?? Brush.Transparent;

        ContainerBorder.Stroke =
            BorderBrush ?? Brush.Transparent;

        ContainerBorder.StrokeThickness =
            BorderWidth;

        Shape.CornerRadius =
            CornerRadius;

        ContainerBorder.Shadow =
            BarShadow;
    }

    #endregion
}


/// <summary>
/// محل قرارگیری TabBar.
/// </summary>
public enum TabBarPlacement
{
    Top,
    Bottom,
    Left,
    Right
}


/// <summary>
/// اطلاعات مربوط به تغییر انتخاب در TabBar.
/// </summary>
public sealed class TabBarSelectionChangedEventArgs : EventArgs
{
    public TabBarSelectionChangedEventArgs(
        int oldIndex,
        int newIndex,
        TabItemView? oldItem,
        TabItemView? newItem)
    {
        OldIndex = oldIndex;
        NewIndex = newIndex;
        OldItem = oldItem;
        NewItem = newItem;
    }


    public int OldIndex { get; }


    public int NewIndex { get; }


    public TabItemView? OldItem { get; }


    public TabItemView? NewItem { get; }
}