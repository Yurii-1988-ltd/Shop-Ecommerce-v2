using Ecommerce.Basket.Modules.Domain.Errors;

namespace Ecommerce.Domain.Domain;

public record BasketQuantity
{
    public int Value { get; }
    public const int MaxPerItem = 99;

    private BasketQuantity(int value)
    {
        Value = value;
    }

    // Static Factory Method
    public static Result<BasketQuantity> Create(int value)
    {
        if (value <= 0)
            return Result.Failure<BasketQuantity>(BasketErrors.InvalidQuantityZeroOrNegative);

        if (value > MaxPerItem)
            return Result.Failure<BasketQuantity>(BasketErrors.QuantityExceedsLimit);

        return Result.Success(new BasketQuantity(value));
    }

    // Method to return a new BasketQuantity with additional delta
    public Result<BasketQuantity> Add(int delta)
    {
        return Create(Value + delta);
    }
}