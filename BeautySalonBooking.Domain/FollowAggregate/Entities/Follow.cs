using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.FollowAggregate.Enums;

namespace BeautySalonBooking.Domain.FollowAggregate.Entities;

public class Follow : AuditableSoftDeleteEntity<long>
{
    public FollowTargetType FollowerType { get; private set; }
    public long FollowerId { get; private set; }

    public FollowTargetType TargetType { get; private set; }
    public long TargetId { get; private set; }

    private Follow()
    {
    }

    public Follow(
        FollowTargetType followerType,
        long followerId,
        FollowTargetType targetType,
        long targetId,
        long createdBy)
        : base(createdBy)
    {
        ValidateType(followerType, nameof(followerType));
        ValidateType(targetType, nameof(targetType));

        if (followerId <= 0)
            throw new ArgumentOutOfRangeException(nameof(followerId));

        if (targetId <= 0)
            throw new ArgumentOutOfRangeException(nameof(targetId));

        if (createdBy <= 0)
            throw new ArgumentOutOfRangeException(nameof(createdBy));

        if (followerType == targetType && followerId == targetId)
        {
            throw new InvalidOperationException(
                "An entity cannot follow itself.");
        }

        FollowerType = followerType;
        FollowerId = followerId;
        TargetType = targetType;
        TargetId = targetId;
    }

    public void Unfollow(long userId)
    {
        if (userId <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId));

        if (!IsActive)
            throw new InvalidOperationException(
                "This follow relationship is already inactive.");

        Delete(userId);
        MarkUpdated(userId);
    }

    private static void ValidateType(
        FollowTargetType type,
        string parameterName)
    {
        if (!Enum.IsDefined(typeof(FollowTargetType), type))
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }
    }
}
