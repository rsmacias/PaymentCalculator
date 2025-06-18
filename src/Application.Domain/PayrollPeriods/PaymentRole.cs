using Application.Domain.Abstractions;
using Application.Domain.Employees;

namespace Application.Domain.PayrollPeriods;

public sealed class PaymentRole : Entity<long>
{
    public PaymentPeriod PaymentPeriod { get; private set; }
    public long PaymentPeriodId { get; private set; }

    public Employee Employee { get; private set; }
    public long EmployeeId { get; private set; }

    public double WorkedHours { get; private set; }
    public double Payment { get; private set; }

    public PaymentRole(
        long id, 
        PaymentPeriod paymentPeriod, 
        Employee employee, 
        double workedHours, 
        double payment) : base(id)
    {
        PaymentPeriod = paymentPeriod;
        PaymentPeriodId = PaymentPeriod.Id;
        Employee = employee;
        EmployeeId = Employee.Id;
        WorkedHours = workedHours;
        Payment = payment;
    }
}
