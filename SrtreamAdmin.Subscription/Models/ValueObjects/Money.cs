namespace StreamAdmin.Subscription.Models.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = null!;

    private Money()
    {
    }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency is required.", nameof(currency));
        }

        var normalizedCurrency = currency.Trim().ToUpperInvariant();

        if (normalizedCurrency.Length != 3 || !normalizedCurrency.All(char.IsLetter))
        {
            throw new ArgumentException("Currency must be a three-letter ISO code.", nameof(currency));
        }

        Amount = amount;
        Currency = normalizedCurrency;
    }
}
