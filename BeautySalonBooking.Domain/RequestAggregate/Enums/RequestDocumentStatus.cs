namespace BeautySalonBooking.Domain.RequestAggregate.Enums;

public enum RequestDocumentStatus : byte
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    NeedRevision = 4,
    Expired = 5
}