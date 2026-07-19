using BeautySalonBooking.Domain.AppointmentAggregate.Enums;
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.BranchAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;

namespace BeautySalonBooking.Domain.AppointmentAggregate.Entities;

public class Appointment : AuditableSoftDeleteEntity<long>
{
    public long CustomerUserId { get; private set; }
    public User CustomerUser { get; private set; } = null!;

    public long BranchMemberServiceId { get; private set; }
    public BranchMemberService BranchMemberService { get; private set; } = null!;

    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public decimal TotalPrice { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public string? Note { get; private set; }
    public long? CancelledByUserId { get; private set; }
    public User? CancelledByUser { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public string? CancelReason { get; private set; }
}