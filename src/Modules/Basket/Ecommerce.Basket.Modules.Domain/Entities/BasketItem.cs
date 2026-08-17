using Ecommerce.Basket.Modules.Domain.Errors;
using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Basket.Modules.Domain.Entities;

public sealed class BasketItem : Entity
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public Money UnitPrice { get; private set; }
    public BasketQuantity Quantity { get; private set; }

    private BasketItem()
    {
    }

    internal BasketItem(
        Guid productId,
        string productName,
        Money unitPrice,
        BasketQuantity quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static Result<BasketItem> Create(
        Guid productId,
        string productName,
        Money unitPrice,
        int quantity)
    {
        if (productId == Guid.Empty)
            return Result.Failure<BasketItem>(
                BasketErrors.EmptyProductId);

        if (string.IsNullOrWhiteSpace(productName))
            return Result.Failure<BasketItem>(
                BasketErrors.EmptyProductName);

        if (unitPrice is null)
            return Result.Failure<BasketItem>(
                BasketErrors.NullUnitPrice);

        var quantityResult = BasketQuantity.Create(quantity);

        if (quantityResult.IsFailure)
            return Result.Failure<BasketItem>(
                quantityResult.Error);

        return Result.Success(
            new BasketItem(
                productId,
                productName,
                unitPrice,
                quantityResult.Value));
    }

    internal Result UpdateQuantity(int newQuantity)
    {
        var quantityResult = BasketQuantity.Create(newQuantity);

        if (quantityResult.IsFailure)
            return quantityResult.Error;

        Quantity = quantityResult.Value;

        return Result.Success();
    }

    public Money LineTotal =>
        UnitPrice * Quantity.Value;
}