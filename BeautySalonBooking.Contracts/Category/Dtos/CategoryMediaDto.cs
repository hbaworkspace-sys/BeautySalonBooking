namespace BeautySalonBooking.Contracts.Category.Dtos;

public sealed class CategoryMediaDto
{
    public long Id { get; set; } // 👈 این خط را اضافه کنید
    public string FileName { get; init; } = string.Empty;

    public string ContentType { get; init; } = string.Empty;

    public long FileSize { get; init; }

    public byte[] Content { get; init; } = Array.Empty<byte>(); // 👈 یا Base64

    public int DisplayOrder { get; init; }
}