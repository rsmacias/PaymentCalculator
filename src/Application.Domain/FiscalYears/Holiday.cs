using Application.Domain.Abstractions;

namespace Application.Domain.FiscalYears;

internal class Holiday : Entity<long>
{
    public long FiscalYearId { get; private set; }
    public FiscalYear FiscalYear { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateOnly Date { get; private set; }

    public Holiday(
        long id, 
        FiscalYear fiscalYear, 
        DateOnly date, 
        string name, 
        string? description) : base(id)
    {
        FiscalYear = fiscalYear;
        FiscalYearId = FiscalYear.Id;
        Date = date;
        Name = name;
        Description = description;
    }
}
