namespace Ecommerce.Admin.ApiClients.Catalog.Contracts
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Currency { get; set; } = string.Empty;
    }
}
