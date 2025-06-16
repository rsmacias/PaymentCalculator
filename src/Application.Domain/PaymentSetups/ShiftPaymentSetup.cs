using Application.Domain.Abstractions;
using Application.Domain.Shared;

namespace Application.Domain.PaymentSetups;

public enum DayTypes
{
    WorkingDay = 1,
    Holiday = 2,
    PaidTimeOff = 3,
    OutOfOffice = 4,
    SickLeave = 5
}

public sealed class ShiftPaymentSetup : Entity<long>, IAggregateRoot
{
    public DayOfWeek Day { get; private set; }
    public DayTypes Type { get; private set; }
    public TimeRange TimeRange { get; private set; }
    public double Payment { get; private set; }
    public bool IsActive { get; private set; }

    public ShiftPaymentSetup(
        long id, 
        DayOfWeek day, 
        DayTypes type, 
        TimeRange timeRange, 
        double payment) : base(id)
    {
        Day = day;
        Type = type;
        TimeRange = timeRange;
        Payment = payment;
        IsActive = true;
    }
}
