namespace Application.Domain.Shared;

public record Money(decimal Amount, Currency Currency)
{
    public static Money Zero() => new Money(0, Currency.None);
    public static Money Zero(Currency currency) => new Money(0, currency);
    public bool IsZero() => this == Zero(Currency);
}
