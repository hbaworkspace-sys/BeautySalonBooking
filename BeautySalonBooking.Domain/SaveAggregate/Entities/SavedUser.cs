
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

namespace BeautySalonBooking.Domain.SaveAggregate.Entities;

public class SavedUser : AuditableEntity<long>
{
    public long? OwnerUserId { get; private set; }
    public long? OwnerOrganizationId { get; private set; }

    public long UserId { get; private set; }
    public User User { get; private set; } = null!;

    private SavedUser()
    {
    }

    public SavedUser(
        long? ownerUserId,
        long? ownerOrganizationId,
        long savedUserId,
        long createdBy)
        : base(createdBy)
    {
        ValidateOwner(ownerUserId, ownerOrganizationId);

        if (savedUserId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(savedUserId),
                "شناسه شخص ذخیره‌شده معتبر نیست.");

        if (createdBy <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(createdBy),
                "شناسه کاربر ایجادکننده معتبر نیست.");

        if (ownerUserId.HasValue &&
            ownerUserId.Value == savedUserId)
            throw new InvalidOperationException(
                "کاربر نمی‌تواند پروفایل خودش را ذخیره کند.");

        OwnerUserId = ownerUserId;
        OwnerOrganizationId = ownerOrganizationId;
        UserId = savedUserId;
    }

    private static void ValidateOwner(
        long? ownerUserId,
        long? ownerOrganizationId)
    {
        bool hasUser = ownerUserId.HasValue;
        bool hasOrganization = ownerOrganizationId.HasValue;

        if (hasUser == hasOrganization)
            throw new ArgumentException(
                "دقیقاً یکی از شناسه‌های کاربر یا سازمان باید مشخص شود.");

        if (ownerUserId.HasValue && ownerUserId.Value <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(ownerUserId),
                "شناسه کاربر معتبر نیست.");

        if (ownerOrganizationId.HasValue &&
            ownerOrganizationId.Value <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(ownerOrganizationId),
                "شناسه سازمان معتبر نیست.");
    }
}
