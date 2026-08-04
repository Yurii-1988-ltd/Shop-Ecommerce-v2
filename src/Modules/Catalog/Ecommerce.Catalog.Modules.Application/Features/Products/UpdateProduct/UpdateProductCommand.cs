using Ecommerce.Application.CQRS;
using Ecommerce.Catalog.Modules.Application.Dto;

namespace Ecommerce.Catalog.Modules.Application.Features.Products.UpdateProduct;

public sealed record UpdateProductCommand(Guid Id,
    string Name,
    string Description,
    string Sku,
    MoneyDto Price)
    : ICommand<Guid>;

