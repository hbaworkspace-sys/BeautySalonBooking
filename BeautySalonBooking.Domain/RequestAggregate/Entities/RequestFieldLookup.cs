using BeautySalonBooking.Domain.Base.Entities;

namespace BeautySalonBooking.Domain.RequestAggregate.Entities;

public class RequestFieldLookup : AuditableSoftDeleteEntity<int>
{
    public string Title { get; private set; } = null!;
    public string Code { get; private set; } = null!;

    public string Schema { get; private set; } = null!;
    public string TableName { get; private set; } = null!;

    public string ValueColumn { get; private set; } = null!;
    public string DisplayColumn { get; private set; } = null!;


    private readonly List<RequestField> _requestFields = new();
    public IReadOnlyCollection<RequestField> RequestFields => _requestFields;
}