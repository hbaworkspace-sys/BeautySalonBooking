using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;

namespace BeautySalonBooking.Domain.CategoryAggregate.Entities;

public class CategoryMedia : AuditableEntity<long>
{
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public string FileName { get; private set; } = null!;
    public string ContentType { get; private set; } = null!;
    public long FileSize { get; private set; }
    public byte[] Content { get; private set; } = null!;
    public MediaType Type { get; private set; }
    public int DisplayOrder { get; private set; }
    // 👇 سازنده خالی برای EF Core
    private CategoryMedia() { }

    // 👇 سازنده کامل برای استفاده در کد (که خطای CS1739 را برطرف می‌کند)
    public CategoryMedia(
        string fileName,
        string contentType,
        byte[] content,
        int displayOrder = 0)
    {
        FileName = fileName;
        ContentType = contentType;
        Content = content;
        FileSize = content.Length; // محاسبه خودکار حجم فایل
        DisplayOrder = displayOrder;
    }
}