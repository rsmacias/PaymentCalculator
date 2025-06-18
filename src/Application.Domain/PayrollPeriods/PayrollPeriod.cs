using Application.Domain.Abstractions;
using Application.Domain.Shared;

namespace Application.Domain.PayrollPeriods;

public enum PayrollPeriodStatus
{
    Pending = 0,
    Ongoing = 1,
    Paid = 2
}

public sealed class PayrollPeriod : Entity<long>, IAggregateRoot
{
    public DateRange DateRange { get; private set; }
    public PayrollPeriodStatus Status { get; private set; }

    private readonly List<Payroll> _payments = new List<Payroll>();
    public IReadOnlyList<Payroll> Payments => _payments.AsReadOnly();

    public PayrollPeriod(
        long id, 
        DateRange dateRange,
        PayrollPeriodStatus status) : base(id)
    {
        DateRange = dateRange;
        Status = status;
    }
}
