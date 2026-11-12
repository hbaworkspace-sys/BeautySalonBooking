
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;

namespace BeautySalonBooking.Domain.SaveAggregate.Entities;

public class SavedOrganization : AuditableEntity<long>
{
    public long? OwnerUserId { get; private set; }
    public long? OwnerOrganizationId { get; private set; }

    public long OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    private SavedOrganization()
    {
    }

    public SavedOrganization(
        long? ownerUserId,
        long? ownerOrganizationId,
        long savedOrganizationId,
        long createdBy)
        : base(createdBy)
    {
        ValidateOwner(ownerUserId, ownerOrganizationId);

        if (savedOrganizationId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(savedOrganizationId),
                "شناسه سازمان ذخیره‌شده معتبر نیست.");

        if (createdBy <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(createdBy),
                "شناسه کاربر ایجادکننده معتبر نیست.");

        if (ownerOrganizationId.HasValue &&
            ownerOrganizationId.Value == savedOrganizationId)
            throw new InvalidOperationException(
                "سازمان نمی‌تواند خودش را ذخیره کند.");

        OwnerUserId = ownerUserId;
        OwnerOrganizationId = ownerOrganizationId;
        OrganizationId = savedOrganizationId;
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
