public sealed record CreateOrderCommand(
    Guid CustomerId,
    string CustomerEmail, // 👈 Добавляем email покупателя
    OrderAddressDto ShippingAddress,
    List<CreateOrderItemDto> Items,
    string Currency) : ICommand<Guid>;