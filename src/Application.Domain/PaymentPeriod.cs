namespace Application.Domain;

public enum PeriodStatus
{
    Pending = 0,
    Ongoing = 1,
    Paid = 2
}

public sealed class PaymentPeriod
{
    public long Id { get; private set; }
    public DateOnly Start { get; private set; }
    public DateOnly End { get; private set; }
    public PeriodStatus Status { get; private set; }

    public PaymentPeriod(
        long id, 
        DateOnly start, 
        DateOnly end, 
        PeriodStatus status)
    {
        Id = id;
        Start = start;
        End = end;
        Status = status;
    }
}
