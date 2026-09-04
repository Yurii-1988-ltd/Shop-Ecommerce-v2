namespace Ecommerce.Storefront.ApiClients.Carts.Models
{
    public sealed record AddCartItemRequest(
        Guid ProductId,
        int Quantity
    );
}
