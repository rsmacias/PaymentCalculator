namespace Application.Domain.Shared;

public record Currency
{
    public string Code { get; init; }

    private Currency(string code)
    {
        Code = code;
    }

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ?? throw new ApplicationException("The currency code is invalid.");
    }

    internal static readonly Currency None = new Currency("");
    public static readonly Currency Usd = new Currency("USD");
    public static readonly Currency Eur = new Currency("EUR");

    public static readonly IReadOnlyCollection<Currency> All = new[]
    {
        Usd,
        Eur
    };
}
