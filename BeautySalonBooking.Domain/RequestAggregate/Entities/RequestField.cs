using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.RequestAggregate.Enums;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class RequestField : AuditableSoftDeleteEntity<int>
{
    public int RequestTypeId { get; private set; }
    public RequestType RequestType { get; private set; } = null!;

    public string Title { get; private set; } = null!;
    public string Key { get; private set; } = null!;
    public RequestFieldType FieldType { get; private set; }
    public bool IsRequired { get; private set; }
    public int DisplayOrder { get; private set; }
    public int? MaxLength { get; private set; }
    public string? DefaultValue { get; private set; }
    public string? Placeholder { get; private set; }
    public string? Description { get; private set; }
    public RequestFieldGroup Group { get; private set; }
    public int? LookupId { get; private set; }
    public RequestFieldLookup? Lookup { get; private set; }


    private readonly List<RequestDetail> _requestDetails = new();
    public IReadOnlyCollection<RequestDetail> RequestDetails => _requestDetails;
}