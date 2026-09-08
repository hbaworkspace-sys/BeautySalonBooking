using BeautySalonBooking.Domain.AppointmentAggregate.Enums;
using BeautySalonBooking.Domain.SchedulingAggregate.ReadModels;

namespace BeautySalonBooking.Domain.SchedulingAggregate.Services;

public sealed class AvailabilityCalculator
{
    private static readonly AppointmentStatus[] BlockingAppointmentStatuses =
    [
        AppointmentStatus.Pending,
        AppointmentStatus.Confirmed,
        AppointmentStatus.CheckedIn,
        AppointmentStatus.InProgress
    ];

    public IReadOnlyList<AvailableDateReadModel> Calculate(
        BookingAvailabilityReadModel availability,
        DateOnly fromDate,
        DateOnly toDate,
        TimeSpan slotInterval)
    {
        ArgumentNullException.ThrowIfNull(availability);

        if (fromDate > toDate)
            throw new ArgumentException(
                "FromDate must be less than or equal to ToDate.");

        if (availability.ServiceDuration <= TimeSpan.Zero)
            throw new ArgumentException(
                "Service duration must be greater than zero.");

        if (slotInterval <= TimeSpan.Zero)
            throw new ArgumentException(
                "Slot interval must be greater than zero.");

        var result = new List<AvailableDateReadModel>();

        for (
            var date = fromDate;
            date <= toDate;
            date = date.AddDays(1))
        {
            var slots = CalculateDateSlots(
                availability,
                date,
                slotInterval);

            if (slots.Count == 0)
                continue;

            result.Add(new AvailableDateReadModel
            {
                Date = date,
                Slots = slots
            });
        }

        return result;
    }

    private IReadOnlyList<AvailableTimeSlotReadModel> CalculateDateSlots(
        BookingAvailabilityReadModel availability,
        DateOnly date,
        TimeSpan slotInterval)
    {
        // -----------------------------------------------------
        // Branch Holiday
        // -----------------------------------------------------

        if (availability.BranchHolidays.Contains(date))
            return [];

        // -----------------------------------------------------
        // Working Intervals
        // -----------------------------------------------------

        var workingIntervals =
            GetWorkingIntervals(
                availability,
                date);

        if (workingIntervals.Count == 0)
            return [];

        // -----------------------------------------------------
        // Unavailable Intervals
        // -----------------------------------------------------

        var unavailableIntervals =
            GetUnavailableIntervals(
                availability,
                date);

        // -----------------------------------------------------
        // Free Intervals
        // -----------------------------------------------------

        var freeIntervals =
            SubtractIntervals(
                workingIntervals,
                unavailableIntervals);

        // -----------------------------------------------------
        // Generate Slots
        // -----------------------------------------------------

        return GenerateSlots(
            freeIntervals,
            availability.ServiceDuration,
            slotInterval);
    }

    private static IReadOnlyList<WorkingIntervalReadModel> GetWorkingIntervals(
        BookingAvailabilityReadModel availability,
        DateOnly date)
    {
        // =====================================================
        // 1. Branch Working Hours
        // =====================================================

        var branchIntervals =
            availability.BranchWorkingHours
                .Where(x =>
                    x.DayOfWeek == date.DayOfWeek &&
                    x.StartTime < x.EndTime)
                .Select(x =>
                    new TimeInterval(
                        x.StartTime,
                        x.EndTime))
                .OrderBy(x => x.Start)
                .ToList();

        if (branchIntervals.Count == 0)
            return [];

        // =====================================================
        // 2. Member Working Schedule
        // =====================================================

        var memberIntervals =
            GetMemberWorkingIntervals(
                availability,
                date);

        if (memberIntervals.Count == 0)
            return [];

        // =====================================================
        // 3. Intersection
        //
        // Member can work only when:
        //
        // Branch is open
        // AND
        // Member is working
        //
        // Branch can restrict Member,
        // but Member cannot open a closed Branch.
        // =====================================================

        var intersection =
            IntersectIntervals(
                branchIntervals,
                memberIntervals);

        return intersection
            .Select(x => new WorkingIntervalReadModel
            {
                DayOfWeek = date.DayOfWeek,
                StartTime = x.Start,
                EndTime = x.End
            })
            .ToList();
    }

    private static IReadOnlyList<TimeInterval> GetMemberWorkingIntervals(
        BookingAvailabilityReadModel availability,
        DateOnly date)
    {
        // =====================================================
        // Schedule Exceptions
        //
        // Exception for the specific date overrides
        // the Member's normal schedule.
        // =====================================================

        var exceptions = availability.ScheduleExceptions
            .Where(x => x.Date == date)
            .ToList();

        if (exceptions.Count > 0)
        {
            // Explicit non-working exception
            // means Member does not work that day.
            if (exceptions.All(x => !x.IsWorkingDay))
                return [];

            return exceptions
                .Where(x =>
                    x.IsWorkingDay &&
                    x.StartTime < x.EndTime)
                .Select(x =>
                    new TimeInterval(
                        x.StartTime,
                        x.EndTime))
                .OrderBy(x => x.Start)
                .ToList();
        }

        // =====================================================
        // Normal Member Schedule
        // =====================================================

        return availability.MemberWorkingShifts
            .Where(x =>
                x.DayOfWeek == date.DayOfWeek &&
                x.StartTime < x.EndTime)
            .Select(x =>
                new TimeInterval(
                    x.StartTime,
                    x.EndTime))
            .OrderBy(x => x.Start)
            .ToList();
    }

    private static IReadOnlyList<TimeInterval> IntersectIntervals(
        IReadOnlyList<TimeInterval> first,
        IReadOnlyList<TimeInterval> second)
    {
        var result = new List<TimeInterval>();

        foreach (var firstInterval in first)
        {
            foreach (var secondInterval in second)
            {
                var start =
                    firstInterval.Start > secondInterval.Start
                        ? firstInterval.Start
                        : secondInterval.Start;

                var end =
                    firstInterval.End < secondInterval.End
                        ? firstInterval.End
                        : secondInterval.End;

                if (start < end)
                {
                    result.Add(
                        new TimeInterval(
                            start,
                            end));
                }
            }
        }

        return MergeIntervals(result);
    }

    private static IReadOnlyList<TimeInterval> GetUnavailableIntervals(
        BookingAvailabilityReadModel availability,
        DateOnly date)
    {
        var intervals = new List<TimeInterval>();

        // -----------------------------------------------------
        // TimeOff
        // -----------------------------------------------------

        foreach (var timeOff in availability.TimeOffs)
        {
            if (date < timeOff.StartDate ||
                date > timeOff.EndDate)
            {
                continue;
            }

            // Full-day TimeOff
            if (timeOff.StartTime is null ||
                timeOff.EndTime is null)
            {
                intervals.Add(
                    new TimeInterval(
                        TimeOnly.MinValue,
                        TimeOnly.MaxValue));

                continue;
            }

            // Time-based TimeOff
            if (timeOff.StartTime < timeOff.EndTime)
            {
                intervals.Add(
                    new TimeInterval(
                        timeOff.StartTime.Value,
                        timeOff.EndTime.Value));
            }
        }

        // -----------------------------------------------------
        // Appointments
        // -----------------------------------------------------

        foreach (var appointment in availability.Appointments)
        {
            if (appointment.Date != date)
                continue;

            if (!BlockingAppointmentStatuses.Contains(
                    appointment.Status))
            {
                continue;
            }

            if (appointment.StartTime >= appointment.EndTime)
                continue;

            intervals.Add(
                new TimeInterval(
                    appointment.StartTime,
                    appointment.EndTime));
        }

        return MergeIntervals(intervals);
    }

    private static IReadOnlyList<AvailableTimeSlotReadModel> GenerateSlots(
        IReadOnlyList<TimeInterval> freeIntervals,
        TimeSpan serviceDuration,
        TimeSpan slotInterval)
    {
        var slots = new List<AvailableTimeSlotReadModel>();

        foreach (var interval in freeIntervals)
        {
            var currentStart = interval.Start;

            while (true)
            {
                var currentEnd =
                    currentStart.Add(serviceDuration);

                // Entire service must fit inside
                // the available interval.
                if (currentEnd > interval.End)
                    break;

                slots.Add(
                    new AvailableTimeSlotReadModel
                    {
                        StartTime = currentStart,
                        EndTime = currentEnd
                    });

                currentStart =
                    currentStart.Add(slotInterval);
            }
        }

        return slots;
    }

    private static IReadOnlyList<TimeInterval> SubtractIntervals(
        IReadOnlyList<WorkingIntervalReadModel> workingIntervals,
        IReadOnlyList<TimeInterval> unavailableIntervals)
    {
        var result = new List<TimeInterval>();

        foreach (var workingInterval in workingIntervals)
        {
            var remaining =
                new List<TimeInterval>
                {
                    new(
                        workingInterval.StartTime,
                        workingInterval.EndTime)
                };

            foreach (var unavailable in unavailableIntervals)
            {
                remaining = remaining
                    .SelectMany(interval =>
                        Subtract(
                            interval,
                            unavailable))
                    .ToList();

                if (remaining.Count == 0)
                    break;
            }

            result.AddRange(remaining);
        }

        return MergeIntervals(result);
    }

    private static IReadOnlyList<TimeInterval> Subtract(
        TimeInterval source,
        TimeInterval unavailable)
    {
        // No overlap
        if (unavailable.End <= source.Start ||
            unavailable.Start >= source.End)
        {
            return [source];
        }

        var result = new List<TimeInterval>();

        // Left side remains available
        if (unavailable.Start > source.Start)
        {
            result.Add(
                new TimeInterval(
                    source.Start,
                    unavailable.Start < source.End
                        ? unavailable.Start
                        : source.End));
        }

        // Right side remains available
        if (unavailable.End < source.End)
        {
            result.Add(
                new TimeInterval(
                    unavailable.End > source.Start
                        ? unavailable.End
                        : source.Start,
                    source.End));
        }

        return result
            .Where(x => x.Start < x.End)
            .ToList();
    }

    private static IReadOnlyList<TimeInterval> MergeIntervals(
        IEnumerable<TimeInterval> intervals)
    {
        var ordered = intervals
            .Where(x => x.Start < x.End)
            .OrderBy(x => x.Start)
            .ToList();

        if (ordered.Count == 0)
            return [];

        var result = new List<TimeInterval>();

        var current = ordered[0];

        for (var i = 1; i < ordered.Count; i++)
        {
            var next = ordered[i];

            if (next.Start <= current.End)
            {
                current = new TimeInterval(
                    current.Start,
                    next.End > current.End
                        ? next.End
                        : current.End);

                continue;
            }

            result.Add(current);
            current = next;
        }

        result.Add(current);

        return result;
    }

    private readonly record struct TimeInterval(
        TimeOnly Start,
        TimeOnly End);
}