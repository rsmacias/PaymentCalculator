using Acme.Timetracker.Domain.Abstractions;

namespace Acme.Timetracker.Domain.Shifts;

public enum ShiftType : int
{
    None = 0,
    LaborDay = 1,
    Weekend = 2,
    Holiday = 3
}

public enum WeekDay : int
{
    None = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7,
}

public sealed class PaymentShift : Entity
{
    public PaymentShift(
        Guid id
    ) : base(id)
    {
        CreatedOnUtc = DateTime.UtcNow;
    }

    public ShiftType Type { get; private set; }
    public WeekDay? WeekDay { get; private set; }
    public bool IsFullDay { get; private set; }
    public TimeOnly StartAt { get; private set; }
    public TimeOnly EndAt { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedOnUtc { get; private set; }
    public DateTimeOffset? UpdateOnUtc { get; private set; }
}
