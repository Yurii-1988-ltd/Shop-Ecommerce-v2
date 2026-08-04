

namespace Ecommerce.Catalog.Modules.Application.Features.Images.ChangeImageOrder;

public sealed record ChangeImageOrderCommand(Guid ProductId, 
    Guid ImageId, int NewIndex ) : ICommand;