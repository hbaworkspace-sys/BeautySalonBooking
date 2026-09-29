namespace BeautySalonBooking.Contracts.Organization.Dtos;

public class OrganizationMediaDto
{
    public string FileName { get; init; } = string.Empty;

    public string ContentType { get; init; } = string.Empty;

    public long FileSize { get; init; }

    public byte[] Content { get; init; } = [];

    public int DisplayOrder { get; init; }
}