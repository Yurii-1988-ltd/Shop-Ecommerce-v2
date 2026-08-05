namespace Ecommerce.Admin.ApiClients.Orders.Models
{
    public sealed record ChangeOrderItemQuantityModel(Guid OrderItemId, int Quantity);

}
