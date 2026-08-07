using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Inventory.Modules.Application.Features.CreateInventoryItem;

internal sealed class CreateInventoryItemCommandHandler : ICommandHandler<CreateInventoryItemCommand, Guid>
{
    public Task<Result<Guid>> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
