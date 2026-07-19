namespace BeautySalonBooking.Domain.RequestAggregate.Enums;

public enum RequestHistoryAction : byte
{
    Created = 1,
    Submitted = 2,
    StatusChanged = 3,

    DetailAdded = 4,
    DetailUpdated = 5,

    DocumentUploaded = 6,
    DocumentDeleted = 7,
    DocumentApproved = 8,
    DocumentRejected = 9,

    ReturnedForRevision = 10,
    PaymentCompleted = 11,

    Approved = 12,
    Rejected = 13,

    Cancelled = 14
}