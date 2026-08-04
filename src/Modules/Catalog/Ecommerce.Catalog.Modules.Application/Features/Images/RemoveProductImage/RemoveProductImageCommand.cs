

namespace Ecommerce.Catalog.Modules.Application.Features.Images.RemoveProductImage;

public sealed record RemoveProductImageCommand(Guid ProductId, Guid ImageId): ICommand;