namespace Ecommerce.Storefront.ApiClients.Carts.Models
{
    public record CreateCartRequest(Guid? CustomerId, Guid? GuestId);

}
