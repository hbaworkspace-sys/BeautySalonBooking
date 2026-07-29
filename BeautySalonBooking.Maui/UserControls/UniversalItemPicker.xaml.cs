using System.Collections;
using Icon = BeautySalonBooking.Maui.Resources;

namespace BeautySalonBooking.Maui.UserControls;

public partial class UniversalItemPicker : ContentView
{
    public event EventHandler<object> Completed;
    public event EventHandler<object> SelectedItemChanged;

    public UniversalItemPicker()
    {
        InitializeComponent();
    }
    public string PlaceHolder
    {
        get { return (string)GetValue(PlaceHolderProperty); }
        set { SetValue(PlaceHolderProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderProperty =
        BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(UniversalItemPicker), default(string));

    public string Glyph
    {
        get { return (string)GetValue(GlyphProperty); }
        set { SetValue(GlyphProperty, value); }
    }
    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(nameof(Glyph), typeof(string), typeof(UniversalItemPicker), Icon.IconSymbols.Person2);

    public string DisplayMemberPath
    {
        get { return (string)GetValue(DisplayMemberPathProperty); }
        set { SetValue(DisplayMemberPathProperty, value); }
    }
    public static readonly BindableProperty DisplayMemberPathProperty =
        BindableProperty.Create(nameof(DisplayMemberPath), typeof(string), typeof(UniversalItemPicker), "Name");

    public IEnumerable List
    {
        get { return (IEnumerable)GetValue(ListProperty); }
        set { SetValue(ListProperty, value); }
    }
    public static readonly BindableProperty ListProperty =
        BindableProperty.Create(nameof(List), typeof(IEnumerable), typeof(UniversalItemPicker), null);

    public object Selected
    {
        get { return (object)GetValue(SelectedProperty); }
        set { SetValue(SelectedProperty, value); }
    }
    public static readonly BindableProperty SelectedProperty =
        BindableProperty.Create(nameof(Selected), typeof(object), typeof(UniversalItemPicker), null);

    private void control_Focused(object sender, FocusEventArgs e)
    {

    }

    private void control_Unfocused(object sender, FocusEventArgs e)
    {

    }

    private void cmbcombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbcombo.SelectedItem != null)
        {
            lblPalceHolder.IsVisible = false;
        }
        else
            lblPalceHolder.IsVisible = true;
    }
}