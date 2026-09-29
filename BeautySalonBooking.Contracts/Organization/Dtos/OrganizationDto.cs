namespace BeautySalonBooking.Contracts.Organization.Dtos;

public class OrganizationDto
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Slogan { get; init; }
    public string? Glyph { get; init; }
    public IReadOnlyList<OrganizationMediaDto> Media { get; init; } = [];
}
