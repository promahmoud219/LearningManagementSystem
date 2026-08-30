namespace LearningManagementSystem.SharedKernel.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency = "EGP")
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpper();
    }

    public static Money Zero(string currency = "EGP") => new(0, currency);

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot subtract money with different currencies.");

        if (left.Amount < right.Amount)
            throw new InvalidOperationException("Cannot have a negative money amount after subtraction.");

        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot add money with different currencies.");

        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public Money ApplyPercentage(decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Percentage must be between 0 and 100.");

        return new Money(this.Amount * (percentage / 100), this.Currency);
    }
}