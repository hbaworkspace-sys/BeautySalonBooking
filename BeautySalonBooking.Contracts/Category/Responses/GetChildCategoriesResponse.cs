using BeautySalonBooking.Contracts.Category.Dtos;

namespace BeautySalonBooking.Contracts.Category.Responses;

public sealed class GetChildCategoriesResponse
{
    public IReadOnlyList<CategoryDto> Categories { get; init; } = [];
}