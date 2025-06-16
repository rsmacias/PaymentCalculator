namespace Application.Domain.Shared;

public record BirthDate(DateOnly Value)
{
    private const int LEGAL_AGE = 18;

    public int GetAge()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var age = today.Year - Value.Year;

        return today.DayOfYear >= Value.DayOfYear ? age : age - 1;
    }

    public bool IsLegalAge() => GetAge() >= LEGAL_AGE;
}
