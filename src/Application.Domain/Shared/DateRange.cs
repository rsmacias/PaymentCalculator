using System.Runtime.CompilerServices;

namespace Application.Domain.Shared;

public record DateRange
{
    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }

    public int TotalDays => End.DayNumber - Start.DayNumber;
    public int TotalMonths 
    { 
        get 
        {
            int months = (End.Year - Start.Year) * 12 + End.Month - Start.Month;
            
            if (End.Day < Start.Day - 1)
            {
                months--;
            }

            if (Start.Day == 1 && DateTime.DaysInMonth(End.Year, End.Month) == End.Day)
            {
                months++;
            }
            
            return months;
        } 
    }

    private DateRange()
    {
        
    }

    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if (start > end)
            throw new ApplicationException("End date precedes start date.");

        return new DateRange
        {
            Start = start,
            End = end
        };
    }
}
