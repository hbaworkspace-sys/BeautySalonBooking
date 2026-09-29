namespace BeautySalonBooking.Contracts.Branch.Branch.Dtos;

public class BranchDto
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? Glyph { get; init; }
    public IReadOnlyList<BranchMediaDto> Media { get; init; } = [];
}