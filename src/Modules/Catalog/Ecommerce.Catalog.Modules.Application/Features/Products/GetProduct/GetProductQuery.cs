using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProduct;

public sealed record GetProductQuery(
    Guid  Id): IQuery<ProductResponse>;
