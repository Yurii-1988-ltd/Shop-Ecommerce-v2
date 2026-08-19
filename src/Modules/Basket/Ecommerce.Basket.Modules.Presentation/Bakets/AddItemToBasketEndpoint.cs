using Ecommerce.Basket.Modules.Application.Features.AddItemToBasket;
using Ecommerce.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Basket.Modules.Presentation.Baskets;

internal sealed class AddItemToBasketEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets/items", async (
            AddItemToBasketRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            // Валидация и создание Money из примитивов
            var moneyResult = Money.Create(request.Price, request.Currency);
            if (moneyResult.IsFailure)
            {
                return Results.BadRequest(moneyResult.Error);
            }

            var command = new AddItemToBasketCommand(
                request.CustomerId,
                request.ProductId,
                request.ProductName,
                moneyResult.Value,
                request.Quantity);

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                 ? Results.Ok(result.Value)
                 : Results.NotFound(result.Error); ;
        })
        .WithTags(Tags.Baskets)
        .WithName("AddItemToBasket");
    }
}
internal sealed record AddItemToBasketRequest(
    Guid CustomerId,
    Guid ProductId,
    string ProductName,
    decimal Price,
    string Currency,
    int Quantity);