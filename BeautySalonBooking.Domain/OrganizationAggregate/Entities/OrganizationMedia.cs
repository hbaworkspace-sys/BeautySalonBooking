using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;

namespace BeautySalonBooking.Domain.OrganizationAggregate.Entities;

public class OrganizationMedia : AuditableEntity<long>
{
    public long OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public string FileName { get; private set; } = null!;
    public string ContentType { get; private set; } = null!;
    public long FileSize { get; private set; }
    public byte[] Content { get; private set; } = null!;
    public MediaType Type { get; private set; }
    public int DisplayOrder { get; private set; }

    // 👇 سازنده خالی برای EF Core
    private OrganizationMedia() { }

    // 👇 سازنده برای استفاده در کد
    public OrganizationMedia(
        string fileName,
        string contentType,
        byte[] content,
        MediaType type,
        int displayOrder = 0)
    {
        FileName = fileName;
        ContentType = contentType;
        Content = content;
        FileSize = content.Length; // 👈 محاسبه خودکار حجم فایل
        Type = type;
        DisplayOrder = displayOrder;
    }
}