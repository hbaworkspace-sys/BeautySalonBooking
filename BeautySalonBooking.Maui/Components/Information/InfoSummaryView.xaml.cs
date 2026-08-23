using BeautySalonBooking.Maui.Components.Information.Models;

namespace BeautySalonBooking.Maui.Components.Information;

public partial class InfoSummaryView : ContentView
{
    public InfoSummaryView()
    {
        InitializeComponent();

        UpdateItems();
    }

    #region Items

    public static readonly BindableProperty ItemsProperty =
        BindableProperty.Create(
            nameof(Items),
            typeof(IList<InfoSummaryItem>),
            typeof(InfoSummaryView),
            default(IList<InfoSummaryItem>),
            propertyChanged: OnItemsChanged);

    public IList<InfoSummaryItem>? Items
    {
        get => (IList<InfoSummaryItem>?)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    private static void OnItemsChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is InfoSummaryView view)
            view.UpdateItems();
    }

    private void UpdateItems()
    {
        ItemsContainer.Clear();

        if (Items is null || Items.Count == 0)
            return;

        foreach (var item in Items)
        {
            var itemView = new InfoSummaryItemView
            {
                Column1 = item.Column1,
                Column2 = item.Column2,
                Column3 = item.Column3,
                IsSeparatorVisible = item.IsSeparatorVisible
            };

            ItemsContainer.Add(itemView);
        }
    }

    #endregion
}