using Application.Domain.Abstractions;
using Application.Domain.Shared;

namespace Application.Domain.FiscalYears;

public class FiscalYear : Entity<long>, IAggregateRoot
{
    public string Code { get; private set; }
    public int Year { get; private set; }
    public DateRange DateRange { get; private set; }

    private readonly List<Holiday> _holidays = new List<Holiday>();
    public IReadOnlyList<Holiday> Holidays => _holidays.AsReadOnly();

    public FiscalYear(
        long id, 
        int year, 
        DateRange dateRange) : base(id)
    {
        Year = year;
        Code = $"FY{Year}";
        DateRange = dateRange;
    }
}
