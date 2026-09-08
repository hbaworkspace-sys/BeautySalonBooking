namespace BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

public sealed class BookingAvailabilityReadModel
{
    public long BranchId { get; init; }

    public long BranchMemberId { get; init; }

    public long BranchMemberServiceId { get; init; }

    public long ServiceId { get; init; }

    public string ServiceTitle { get; init; } = string.Empty;

    public string OrganizationTitle { get; init; } = string.Empty;

    public string BranchTitle { get; init; } = string.Empty;

    public string BranchDescription { get; init; } = string.Empty;

    public string StylistName { get; init; } = string.Empty;

    public decimal ServicePrice { get; init; }

    public TimeSpan ServiceDuration { get; init; }

    public IReadOnlyList<WorkingIntervalReadModel> MemberWorkingShifts { get; init; } = [];

    public IReadOnlyList<WorkingIntervalReadModel> BranchWorkingHours { get; init; } = [];

    public IReadOnlyList<ScheduleExceptionReadModel> ScheduleExceptions { get; init; } = [];

    public IReadOnlyList<TimeOffReadModel> TimeOffs { get; init; } = [];

    public IReadOnlySet<DateOnly> BranchHolidays { get; init; } =
        new HashSet<DateOnly>();

    public IReadOnlyList<AppointmentSlotReadModel> Appointments { get; init; } = [];
}