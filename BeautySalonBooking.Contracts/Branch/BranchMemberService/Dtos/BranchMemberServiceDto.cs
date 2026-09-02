namespace BeautySalonBooking.Contracts.Branch.BranchMemberService.Dtos;

public sealed class BranchMemberServiceDto
{
    public long BranchMemberId { get; init; }

    public long BranchMemberServiceId { get; init; }

    public long PersonId { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string FullName =>
        $"{FirstName} {LastName}";

    public decimal Price { get; init; }

    public TimeSpan Duration { get; init; }
}