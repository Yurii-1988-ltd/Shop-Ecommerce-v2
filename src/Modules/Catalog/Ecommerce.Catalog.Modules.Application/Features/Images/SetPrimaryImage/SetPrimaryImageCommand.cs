

namespace Ecommerce.Catalog.Modules.Application.Features.Images.SetPrimaryImage;

public sealed record SetPrimaryImageCommand(Guid ProductId, Guid ImageId): ICommand;
