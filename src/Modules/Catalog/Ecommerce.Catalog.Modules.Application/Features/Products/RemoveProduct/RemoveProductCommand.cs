

namespace Ecommerce.Catalog.Modules.Application.Features.Products.RemoveProduct;

public sealed record RemoveProductCommand(Guid Id) : Ecommerce.Application.CQRS.ICommand;
