namespace Ecommerce.Admin.ApiClients.Orders.Contracts;

public sealed record CreateOrderRequest(
  Guid CustomerId,
  string CustomerEmail,
  AddressRequest ShippingAddress,
  string Currency);
