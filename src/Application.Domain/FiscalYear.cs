using Application.Domain.Abstractions;

namespace Application.Domain;

internal class FiscalYear : Entity<long>
{
    public string Code { get; private set; }
    public int Year { get; private set; }
    public DateOnly Start { get; private set; }
    public DateOnly End { get; private set; }

    private readonly List<Holiday> _holidays = new List<Holiday>();
    public IReadOnlyList<Holiday> Holidays => _holidays.AsReadOnly();

    public FiscalYear(
        long id, 
        int year, 
        DateOnly start, 
        DateOnly end) : base(id)
    {
        Year = year;
        Code = $"FY{Year}";
        Start = start;
        End = end;
    }
}
