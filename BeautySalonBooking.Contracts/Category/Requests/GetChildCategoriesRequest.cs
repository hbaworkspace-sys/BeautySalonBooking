namespace BeautySalonBooking.Contracts.Category.Requests;

public sealed class GetChildCategoriesRequest
{
    public string ParentCode { get; init; } = string.Empty;
}