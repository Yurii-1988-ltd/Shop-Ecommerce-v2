namespace Ecommerce.Admin.ApiClients.Carts.Models;

public sealed class ChangeQuantityFormModel
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
