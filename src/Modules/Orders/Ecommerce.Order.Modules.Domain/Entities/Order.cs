namespace Ecommerce.Order.Modules.Domain.Entities;

public sealed class Order: Entity
{

    #region  fields and constructors
    private  List<OrderItem> _items = new();

    public Guid CustomerId { get;private set; }
    public string OrderNumber { get; private set; } = default!;
    public OrderStatus Status { get;private set; }
    public OrderAddress  ShippingAddress { get;private set; }
    public string Currency { get;private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime ? UpdatedAtUtc { get; private set; }
    public DateTime? PaidAtUtc { get; private set; }
    public DateTime? ShippedAtUtc { get; private set; }
    public DateTime? CancelledAtUtc { get; private set; }
    public string? CancellationReason { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public int TotalQuantity => _items.Sum(item => item.Quantity);
    #endregion
#region static factory methods
    private Order()
    {
        
    }
    private Order(
     Guid id,
     Guid customerId,
     string orderNumber,
     OrderAddress shippingAddress,
     string currency = CurrencyConstant.UAH)
    {
        Id = id;
        CustomerId = customerId;
        OrderNumber = orderNumber;

        ShippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress));

        Currency = string.IsNullOrWhiteSpace(currency)
            ? CurrencyConstant.USD
            : currency.ToUpperInvariant();

        Status = OrderStatus.Draft;
        CreatedAtUtc = DateTime.UtcNow;
    }
    public static Result<Order> Create(
     Guid customerId,
     string orderNumber,
     OrderAddress shippingAddress,
     string currency = CurrencyConstant.UAH)
    {
        if (customerId == Guid.Empty)
            return OrderErrors.GuidEmpty;

        if (string.IsNullOrWhiteSpace(orderNumber))
            return OrderErrors.OrderNumberIsRequired(orderNumber);

        var order = new Order(
            Guid.NewGuid(),
            customerId,
            orderNumber,
            shippingAddress,
            currency);

        order.AddDomainEvent(
            new OrderCreatedDomainEvent(order.Id, order.CustomerId));

        return Result.Success(order);
    }

    public Result<Money> GetTotalAmount()
    {
        if (!_items.Any())
            return Money.Create(0, CurrencyConstant.UAH);

        Money total = _items[0].TotalPrice;

        for (int i = 1; i < _items.Count; i++)
        {
            var result = total.Add(_items[i].TotalPrice);

            if (result.IsFailure)
                return Result.Failure<Money>(result.Error);

            total = result.Value;
        }

        return total;
    }
 
    public Result AddItem(
    Guid productId,
    string productName,
    string sku,
    Money price,
    int quantity)
    {
       
        var existing = _items.FirstOrDefault(x => x.ProductId == productId);

        if (existing is not null)
        {
            return existing.AddQuantity(quantity);
        }

        var itemResult = OrderItem.Create(
            productId,
            productName,
            sku,
            price,
            quantity);

        if (itemResult.IsFailure)
            return itemResult.Error;

        _items.Add(itemResult.Value);

        return Result.Success();
    }

    public Result RemoveItem(Guid orderItemId)
    {
        var item = _items.FirstOrDefault(x => x.Id == orderItemId);
        if (item is null)
        {
            return Result.Failure(OrderErrors.NotFound(orderItemId));
        }
        _items.Remove(item);
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }
    public Result ChangeStatus(OrderStatus newStatus)
    {
        if (Status == newStatus)
        {
            return Result.Success();
        }

        if (!IsValidTransition(Status, newStatus))
        {
            return OrderErrors.InvalidStatusTransition(Status, newStatus);
        }

        var oldStatus = Status;

        Status = newStatus;
        UpdatedAtUtc = DateTime.UtcNow;

        AddDomainEvent(new OrderStatusChangedDomainEvent(
            Id,
            oldStatus,
            newStatus));

        return Result.Success();
    }
    private static bool IsValidTransition(OrderStatus current, OrderStatus next)
    {
        return current switch
        {
            // 1. Черновик: можно отправить на оформление (Pending) или отменить (Cancelled)
            OrderStatus.Draft => next is OrderStatus.Pending or OrderStatus.Cancelled,

            // 2. Ожидает оплаты: можно оплатить (Paid) или отменить (Cancelled)
            OrderStatus.Pending => next is OrderStatus.Paid or OrderStatus.Cancelled,

            // 3. Оплачен: передается в комплектацию/обработку (Processing)
            OrderStatus.Paid => next is OrderStatus.Processing,

            // 4. В обработке: передается в доставку (Shipped)
            OrderStatus.Processing => next is OrderStatus.Shipped,

            // 5. Отправлен: переходит в доставлен (Delivered)
            OrderStatus.Shipped => next is OrderStatus.Delivered,

            // 6. Доставлен: доступен только возврат (Refunded)
            OrderStatus.Delivered => next is OrderStatus.Refunded,

            // Финальные статусы — переходы из них запрещены
            OrderStatus.Cancelled => false,
            OrderStatus.Refunded => false,

            _ => false
        };
    }
    public Result ForceChangeStatus(OrderStatus newStatus, Guid changeBy, string reason)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);
        if (Status == newStatus)
        {
            return Result.Success();
        }
        var oldStatus = Status;
        Status = newStatus;
        UpdatedAtUtc = DateTime.UtcNow;
        AddDomainEvent(new OrderStatusForceChangedDomainEvent(
            Id,
            oldStatus,
            newStatus,
            changeBy,
            reason
            ));
        return Result.Success();
    }


    public Result Submit()
    {
        if (Status != OrderStatus.Draft)
            return OrderErrors.InvalidStatusTransition(Status, OrderStatus.Pending);

        if (!_items.Any())
            return OrderErrors.EmptyOrder();

        if (ShippingAddress is null)
            return OrderErrors.ShippimhAddressRequired();

        Status = OrderStatus.Pending;
        UpdatedAtUtc = DateTime.UtcNow;

        return Result.Success();
    }
    public Result Cancel()
    {
        if (Status == OrderStatus.Delivered)
            return OrderErrors.OrderAlreadyDelivered();
        if (Status == OrderStatus.Cancelled)
            return OrderErrors.OrderAlreadyCanceled();
        Status = OrderStatus.Cancelled;
        UpdatedAtUtc = DateTime.UtcNow;
        AddDomainEvent(new OrderCancelledDomainEvent(Id));
        return Result.Success();
    }
    public Result UpdateShppingAddress(OrderAddress shippingAddress)
    {
        ArgumentNullException.ThrowIfNull(shippingAddress);
        ShippingAddress = shippingAddress;
        UpdatedAtUtc = DateTime.UtcNow;
        AddDomainEvent(new OrderShippingAddressUpdatedDomainEvent(Id));
        return Result.Success();
    }
    public Result ChangeItemQuantity(Guid orderItemId, int quantity)
    {
        if(Status!= OrderStatus.Draft)
        {
            return OrderErrors.OrderCannotModdified(Status);
        }
        if(quantity <=0)
        {
            return OrderErrors.NegativeQuantity;
        }
        var item = _items.FirstOrDefault(x=>x.Id == orderItemId);
        if (item is null)
        {
            return OrderErrors.OrderItemNotFound(orderItemId);
        }
        item.ChangeQuantity(quantity);
        UpdatedAtUtc= DateTime.UtcNow;
        return Result.Success();

    }
    public Result RemoveOrderItem(Guid orderItemId)
    {
        var item = _items.FirstOrDefault(x => x.Id == orderItemId);

        if (item is null)
        {
            return OrderErrors.OrderItemNotFound(orderItemId);
        }

        _items.Remove(item);

        UpdatedAtUtc = DateTime.UtcNow;

        AddDomainEvent(new OrderItemRemovedDomainEvent(
            Id,
            item.Id,
            item.ProductId));

        return Result.Success();
    }

    #endregion


}

