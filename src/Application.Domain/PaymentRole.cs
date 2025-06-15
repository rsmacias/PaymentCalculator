namespace Application.Domain;

public sealed class PaymentRole
{
    public long Id { get; private set; }

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
        double payment)
    {
        Id = id;
        PaymentPeriod = paymentPeriod;
        PaymentPeriodId = PaymentPeriod.Id;
        Employee = employee;
        EmployeeId = Employee.Id;
        WorkedHours = workedHours;
        Payment = payment;
    }
}
