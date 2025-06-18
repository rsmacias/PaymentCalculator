using Application.Domain.Abstractions;
using Application.Domain.PayrollPeriods;
using Application.Domain.Shared;
using FluentResults;

namespace Application.Domain.FiscalYears;

public enum FiscalYearStatus
{
    Standby = 1,
    Ongoing = 2,
    Finished = 3
}

public class FiscalYear : Entity<long>, IAggregateRoot
{
    public string Code { get; private set; }
    public int Year { get; private set; }
    public DateRange DateRange { get; private set; }
    public FiscalYearStatus Status { get; private set; }

    private readonly List<Holiday> _holidays = new List<Holiday>();
    public IReadOnlyList<Holiday> Holidays => _holidays.AsReadOnly();

    private readonly List<PayrollPeriod> _payrollPeriods = new List<PayrollPeriod>();
    public IReadOnlyList<PayrollPeriod> PayrollPeriods => _payrollPeriods.AsReadOnly();

    private FiscalYear(
        long id, 
        int year, 
        DateRange dateRange) : base(id)
    {
        Year = year;
        Code = $"FY{Year}";
        DateRange = dateRange;
        Status = FiscalYearStatus.Standby;
    }

    public static Result<FiscalYear> Create(int year, DateRange dateRange)
    {
        if (dateRange.TotalMonths != 12)
            return Result.Fail("Invalid dates range for the fiscal year.");

        var fiscalYear = new FiscalYear(0L, year, dateRange);

        return Result.Ok(fiscalYear);
    }

    public void Start()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        if (Status == FiscalYearStatus.Standby && today.IsBetween(DateRange))
        {
            Status = FiscalYearStatus.Ongoing;
        }
    }

    public void Finish()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        if(Status == FiscalYearStatus.Ongoing  && today > DateRange.End)
        {
            Status = FiscalYearStatus.Finished;
        }
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
