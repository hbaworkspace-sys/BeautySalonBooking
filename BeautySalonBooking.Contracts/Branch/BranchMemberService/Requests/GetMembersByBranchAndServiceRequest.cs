namespace BeautySalonBooking.Contracts.Branch.BranchMemberService.Requests;

public sealed class GetMembersByBranchAndServiceRequest
{
    public long BranchId { get; init; }
    public long ServiceId { get; init; }
}