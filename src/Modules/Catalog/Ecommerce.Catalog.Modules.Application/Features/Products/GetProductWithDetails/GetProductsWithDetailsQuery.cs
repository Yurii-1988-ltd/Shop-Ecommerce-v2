

using Ecommerce.Application.CQRS;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.GetProductWithDetails;

public sealed record GetProductsWithDetailsQuery(Guid Id) : IQuery<ProductDetailsResponse>;

