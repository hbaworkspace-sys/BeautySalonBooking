namespace BeautySalonBooking.Domain.RequestAggregate.Enums;

public enum RequestStatus : byte
{
    Draft = 1,
    Submitted = 2,
    UnderReview = 3,
    NeedRevision = 4,
    Revised = 5,
    Approved = 6,
    Rejected = 7,
    Cancelled = 8
}