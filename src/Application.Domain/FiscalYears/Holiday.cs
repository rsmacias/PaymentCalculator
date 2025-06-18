using Application.Domain.Abstractions;
using FluentResults;

namespace Application.Domain.FiscalYears;

public class Holiday : Entity<long>
{
    public long FiscalYearId { get; private set; }
    public FiscalYear FiscalYear { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateOnly Date { get; private set; }

    private Holiday(
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

    public static Result<Holiday> Create(
        FiscalYear fiscalYear, 
        DateOnly date, 
        string name, 
        string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Holiday name is not valid.");

        if (!date.IsBetween(fiscalYear.DateRange))
            return Result.Fail($"Holiday date '{date.ToString("MM-dd-yyyy")}' is not valid for the fiscal year '{fiscalYear.Code}'.");

        var today = DateOnly.FromDateTime(DateTime.Today);

        if (date <= today)
            return Result.Fail($"It is too late to set this date '{date.ToString("MM-dd-yyyy")}' as holiday.");

        var holiday = new Holiday(0L, fiscalYear, date, name, description);

        return Result.Ok(holiday);
    }
}
