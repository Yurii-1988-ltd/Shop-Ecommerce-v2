namespace Ecommerce.Catalog.Modules.Application.Features.Products.CreateProduct;

public sealed record CreateCatalogCommand(
    string Name,
  
    string ProductNumber,
    string Description,
    string Sku,
    decimal Price,
    string Currency
   ) : ICommand<Guid>;

