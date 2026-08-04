namespace Ecommerce.Admin.ApiClients.Carts.Contracts;

public record ChangeCartItemQuantityRequest(Guid CustomerId,Guid ProductId, int Quantity);

