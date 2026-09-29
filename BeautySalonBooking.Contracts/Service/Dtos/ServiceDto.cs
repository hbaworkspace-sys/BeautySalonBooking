namespace BeautySalonBooking.Contracts.Service.Dtos;

public sealed class ServiceDto
{
    public long Id { get; init; }

    public int CategoryId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    public string? Description { get; init; }

    public decimal BasePrice { get; init; }

    public TimeSpan BaseDuration { get; init; }

    public IReadOnlyList<ServiceMediaDto> Media { get; init; } = [];

    //public Image? ImageSource
    //{
    //    get
    //    {
    //        var media = Media
    //            .OrderBy(x => x.DisplayOrder)
    //            .FirstOrDefault();

    //        if (media?.Content is null || media.Content.Length == 0)
    //            return null;

    //        return ImageSource.FromStream(
    //            () => new MemoryStream(media.Content));
    //    }
    //}
}