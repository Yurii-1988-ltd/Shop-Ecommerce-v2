namespace Ecommerce.Admin.ApiClients.Orders.Responses
{
    public sealed record OrderItemResponse(Guid ProductId,
      string ProductName,
      string SKU,
      decimal Amount,
      string Currency,
      int Quantity,
      decimal TotalPrice);
}
