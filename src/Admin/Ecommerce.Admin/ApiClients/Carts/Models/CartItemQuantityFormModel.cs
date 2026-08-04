namespace Ecommerce.Admin.ApiClients.Carts.Models
{
    public sealed class CartItemQuantityFormModel
    {
        public Guid CustomerId { get; set; }
        public  Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
