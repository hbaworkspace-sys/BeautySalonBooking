namespace BeautySalonBooking.Domain.AppointmentAggregate.Enums;

public enum PaymentStatus : byte
{
    Unpaid = 1,
    Pending = 2,
    PartiallyPaid = 3,
    Paid = 4,
    Refunded = 5,
    PartiallyRefunded = 6,
    Failed = 7,
    Cancelled = 8
}