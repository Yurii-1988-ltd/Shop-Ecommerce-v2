namespace Ecommerce.Admin.ApiClients.Carts.Models;

public sealed class CartItemFormModel
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "UAH";
    public int Quantity { get; set; } = 1;

}