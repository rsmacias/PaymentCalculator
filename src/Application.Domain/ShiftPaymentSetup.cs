using Application.Domain.Abstractions;

namespace Application.Domain;

public enum DayTypes
{
    WorkingDay = 1,
    Holiday = 2,
    PaidTimeOff = 3,
    OutOfOffice = 4,
    SickLeave = 5
}

public sealed class ShiftPaymentSetup : Entity<long>
{
    public DayOfWeek Day { get; private set; }
    public DayTypes Type { get; private set; }
    public TimeOnly StartHour { get; private set; }
    public TimeOnly EndHour { get; private set; }
    public double Payment { get; private set; }
    public bool IsActive { get; private set; }

    public ShiftPaymentSetup(
        long id, 
        DayOfWeek day, 
        DayTypes type, 
        TimeOnly startHour, 
        TimeOnly endHour, 
        double payment) : base(id)
    {
        Day = day;
        Type = type;
        StartHour = startHour;
        EndHour = endHour;
        Payment = payment;
        IsActive = true;
    }
}
