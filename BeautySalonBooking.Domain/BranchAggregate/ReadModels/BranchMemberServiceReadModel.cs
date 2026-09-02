namespace BeautySalonBooking.Domain.BranchAggregate.ReadModels;

public sealed class BranchMemberServiceReadModel
{
    public long BranchMemberId { get; init; }

    public long BranchMemberServiceId { get; init; }

    public long PersonId { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public TimeSpan Duration { get; init; }
}