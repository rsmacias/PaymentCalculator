using Application.Domain.Abstractions;
using Application.Domain.Shared;

namespace Application.Domain.PaymentPeriods;

public enum PeriodStatus
{
    Pending = 0,
    Ongoing = 1,
    Paid = 2
}

public sealed class PaymentPeriod : Entity<long>, IAggregateRoot
{
    public DateRange DateRange { get; private set; }
    public PeriodStatus Status { get; private set; }

    private readonly List<PaymentRole> _payments = new List<PaymentRole>();
    public IReadOnlyList<PaymentRole> Payments => _payments.AsReadOnly();

    public PaymentPeriod(
        long id, 
        DateRange dateRange,
        PeriodStatus status) : base(id)
    {
        DateRange = dateRange;
        Status = status;
    }
}
