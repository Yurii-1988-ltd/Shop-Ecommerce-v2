using Ecommerce.Domain.Domain;
using Ecommerce.Inventory.Modules.Domain.Errors;
using Ecommerce.Inventory.Modules.Domain.Events;

namespace Ecommerce.Inventory.Modules.Domain.Entities;

public sealed class InventoryItem : Entity
{
    #region Properties
    public Guid ProductId { get; private set; }
    public string SKU { get; private set; }
    public int MinimumQuantity { get; private set; }
    public DateTime UpdatedUtc { get; private set; }

    public int OnHandQuantity { get; private set; }
    public int ReservedQuantity { get; private set; }

    // Вычисляемое свойство (уменьшается автоматически при увеличении ReservedQuantity)
    public int AvailableQuantity => OnHandQuantity - ReservedQuantity;
    #endregion

    private InventoryItem() { }

    public InventoryItem(Guid productId, string sku, int onHandQuantity, int reservedQuantity, int minimumQuantity, DateTime updatedUtc)
    {
        ProductId = productId;
        SKU = sku;
        OnHandQuantity = onHandQuantity;
        ReservedQuantity = reservedQuantity;
        MinimumQuantity = minimumQuantity;
        UpdatedUtc = updatedUtc;
    }

    #region Domain Methods

    public Result Reserve(int quantity)
    {
        if (quantity <= 0)
            return InventoryErrors.InvalidQuantity;

        if (AvailableQuantity < quantity)
            return InventoryErrors.NotEnoughStock;

        // OnHand не меняется, увеличиваем только резерв (Available уменьшится автоматически)
        ReservedQuantity += quantity;
        UpdatedUtc = DateTime.UtcNow;

        AddDomainEvent(
            new StockReservedDomainEvent(
                Id,
                ProductId,
                quantity));

        return Result.Success();
    }

    public Result Deduct(int quantity)
    {
        if (quantity <= 0)
            return InventoryErrors.InvalidQuantity;

        if (AvailableQuantity < quantity)
            return InventoryErrors.NotEnoughStock;

        // Прямое списание уменьшает фактический остаток
        OnHandQuantity -= quantity;
        UpdatedUtc = DateTime.UtcNow;

        AddDomainEvent(
            new StockDeductedDomainEvent(
                Id,
                ProductId,
                quantity));

        return Result.Success();
    }

    public Result CommitReservation(int quantity)
    {
        if (quantity <= 0)
            return InventoryErrors.InvalidQuantity;

        if (ReservedQuantity < quantity)
            return InventoryErrors.NotEnoughReservedStock;

        // Списываем и зарезервированный, и фактический остаток (товар уехал)
        ReservedQuantity -= quantity;
        OnHandQuantity -= quantity;
        UpdatedUtc = DateTime.UtcNow;

        AddDomainEvent(
            new ReservationCommittedDomainEvent(
                Id,
                ProductId,
                quantity));

        return Result.Success();
    }
    public Result CancelReservation(int quantity)
    {
        if (quantity <= 0)
            return InventoryErrors.InvalidQuantity;

        if (ReservedQuantity < quantity)
            return InventoryErrors.NotEnoughReservedStock;

        ReservedQuantity -= quantity;
        UpdatedUtc = DateTime.UtcNow;

        AddDomainEvent(
            new ReservationCancelledDomainEvent(
                Id,
                ProductId,
                quantity));

        return Result.Success();
    }
    public Result Replenish(int quantity)
    {
        if (quantity <= 0)
            return InventoryErrors.InvalidQuantity;

        OnHandQuantity += quantity;
        UpdatedUtc = DateTime.UtcNow;

        AddDomainEvent(
            new StockReplenishedDomainEvent(
                Id,
                ProductId,
                quantity));

        return Result.Success();
    }


    #endregion
}