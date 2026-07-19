namespace BeautySalonBooking.Maui.UserControls;

public class UniversalItemDTO
{
    public Guid ID;
    public int ID_int;
    public string Display { get; set; }
    public string Secondary { get; set; }
    public object OriginalItem { get; set; }

    public bool IsNewItem { get; set; } = true;
}
