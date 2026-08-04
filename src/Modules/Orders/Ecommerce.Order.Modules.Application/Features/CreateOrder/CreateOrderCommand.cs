public sealed record CreateOrderCommand(
       Guid CustomerId,
    OrderAddressDto ShippingAddress, // 👈 Здесь ДОЛЖЕН быть OrderAddressDto, а не OrderAddress!
    List<CreateOrderItemDto> Items,
    string Currency) : ICommand<Guid>;