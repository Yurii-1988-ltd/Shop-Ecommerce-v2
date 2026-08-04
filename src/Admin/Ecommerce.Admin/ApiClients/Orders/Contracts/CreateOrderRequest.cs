namespace Ecommerce.Admin.ApiClients.Orders.Contracts;

public sealed record CreateOrderRequest(
  Guid CustomerId,
  AddressRequest ShippingAddress,
  string Currency);
