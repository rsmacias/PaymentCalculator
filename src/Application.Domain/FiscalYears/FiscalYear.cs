using Application.Domain.Abstractions;
using Application.Domain.Shared;
using FluentResults;

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

    public Result<List<Holiday>> AddHoliday(
        int days, 
        DateOnly startOn, 
        string name, 
        string? description)
    {
        var holidays = new List<Holiday>();

        for (int i = 0; i < days; i++)
        {
            var holidayDate = startOn.AddDays(i);

            if (_holidays.Any(x => x.Date == holidayDate))
                return Result.Fail($"There is already a holiday configured for the date '{holidayDate.ToString("MM-dd-yyyy")}'");

            var holidayCreation = Holiday.Create(this, holidayDate, name, description);

            if (holidayCreation.IsFailed)
                return Result.Fail(holidayCreation.Errors.First());

            var holiday = holidayCreation.Value;

            holidays.Add(holiday);
        }

        _holidays.AddRange(holidays);

        return Result.Ok(holidays);
    }
}
