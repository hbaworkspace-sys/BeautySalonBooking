using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Enums;

namespace BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

public class UserMedia : AuditableEntity<long>
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string FileName { get; private set; } = null!;
    public string ContentType { get; private set; } = null!;
    public long FileSize { get; private set; }
    public byte[] Content { get; private set; } = null!;
    public MediaType Type { get; private set; }
    public int DisplayOrder { get; private set; }
}
