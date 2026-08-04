using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Modules.Application.Features.UpdateShippingAddress;

internal sealed class UpdateShippingAddressCommandHandler(IOrderRepository repository) : ICommandHandler<UpdateShippingAddressCommand>
{
    public async Task<Result> Handle(UpdateShippingAddressCommand request, CancellationToken cancellationToken)
    {
       var order = await repository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null) 
            return OrderErrors.NotFound(request.OrderId);
        var orderResult = order.UpdateShppingAddress(request.ShippingAddress);
        if (orderResult.IsFailure)
            return orderResult;
        return await repository.UpdateAsync(order, cancellationToken);
    }
}
