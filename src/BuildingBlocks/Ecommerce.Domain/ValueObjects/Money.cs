using Ecommerce.Domain.Domain;
using Ecommerce.Domain.Errors;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Domain.ValueObjects;

public sealed class Money : ValueObject
{
 


    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = string.Empty;

    public Money()
    {
    }

    [BsonConstructor]
    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }





    public static Result<Money> Create(decimal amount, string currency)
    {
        if (amount < 0)
        {
            return MoneyErrors.NegativeAmount;
        }

        if (string.IsNullOrWhiteSpace(currency) || currency.Trim().Length != 3)
        {
            return MoneyErrors.InvalidCurrency;
        }

        return new Money(amount, currency.Trim().ToUpperInvariant());
    }

    public  Result<Money> Add(Money other)
    {
        if (Currency != other.Currency)
        {
            return MoneyErrors.CurrencyMismatch;
        }

        return new Money(Amount + other.Amount, Currency);
    }

    public Result<Money> Subtract(Money other)
    {
        if (Currency != other.Currency)
        {
            return MoneyErrors.CurrencyMismatch;
        }

        return new Money(Amount - other.Amount, Currency);
    }

    public Result<Money> Multiply(decimal multiplier)
    {
        if (multiplier < 0)
        {
            return MoneyErrors.NegativeAmount;
        }

        return new Money(Amount * multiplier, Currency);
    }
    public bool IsGreaterThan(Money other)
    {
        EnsureSameCurrency(other);
        return Amount > other.Amount;
    }

    public bool IsLessThan(Money other)
    {
        EnsureSameCurrency(other);
        return Amount < other.Amount;
    }

    private Result EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
        {
            return MoneyErrors.EnsureSameCurrency;
        }
        return Result.Success();

    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public Result<Money> Multiply(int quantity)
    {
        if (quantity <= 0)
        {
            return MoneyErrors.QuantityMustBePositive;
        }

        return new Money(Amount * quantity, Currency);
    }

    public override string ToString()
    {
        return $"{Amount:0.00} {Currency}";
    }
}