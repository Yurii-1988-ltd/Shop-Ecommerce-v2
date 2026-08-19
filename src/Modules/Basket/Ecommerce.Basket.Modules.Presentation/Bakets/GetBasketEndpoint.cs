using Ecommerce.Basket.Modules.Application.Features.GetBasketByCustomer;

using Ecommerce.Basket.Modules.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Basket.Modules.Presentation.Baskets;

internal sealed class GetBasketEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/baskets/{customerId:guid}", async (
            Guid customerId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetBasketByCustomerIdQuery(customerId);

            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Baskets)
        .WithName("GetBasketByCustomerId")
        .Produces<BasketResponse>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound);
    }
}