namespace BeautySalonBooking.Contracts.Category.Dtos;

public sealed class CategoryDto
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    public string? Description { get; init; }

    public int DisplayOrder { get; init; }
}
