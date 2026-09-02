using BeautySalonBooking.Domain.OrganizationAggregate.Enums;

namespace BeautySalonBooking.Domain.BranchAggregate.ReadModels;

public sealed class BranchServiceOrganizationReadModel
{
    public long BranchServiceId { get; init; }

    public long BranchId { get; init; }
    public string BranchTitle { get; init; } = string.Empty;
    public string? BranchDescription { get; init; }

    public long OrganizationId { get; init; }
    public string OrganizationTitle { get; init; } = string.Empty;
    public OrganizationType OrganizationType { get; init; }

    public decimal Price { get; init; }
    public TimeSpan Duration { get; init; }
    public int DisplayOrder { get; init; }
}