using Ecommerce.Order.Modules.Application.Features.UpdateShippingAddress;
using Ecommerce.Order.Modules.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Order.Modules.Presentation.Orders;

internal sealed class UpdateShippingAddressEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/orders/{orderId:guid}/shipping-address", async (
     Guid orderId,
     UpdateShippingAddressRequest request,
     ISender sender,
     CancellationToken cancellationToken) =>
        {
            var address = new OrderAddress(
                request.FirstName,
                request.LastName,
                request.Country,
                request.City,
                request.Street,
                request.ZipCode);

            var command = new UpdateShippingAddressCommand(
                orderId,
                address);

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.NoContent()
                : Results.BadRequest(result.Error);
        })
            .WithTags(Tags.Orders);
    }
}
public sealed record UpdateShippingAddressRequest(
    string FirstName,
    string LastName,
    string Country,
    string City,
    string Street,
    string ZipCode);

