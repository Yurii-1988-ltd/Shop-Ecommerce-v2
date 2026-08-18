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
            HttpContext httpContext,
            CancellationToken cancellationToken) =>
        {
            // 1. Извлекаем CustomerId из JWT Claims (или заменяем на временный ID из куки/заголовка для гостей)
            var customerIdClaim = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(customerIdClaim, out var customerId))
            {
                return Results.Unauthorized();
            }

     
            var moneyResult = Money.Create(request.Price, request.Currency);
            if (moneyResult.IsFailure)
            {
                return Results.BadRequest(moneyResult.Error);
            }

            var command = new AddItemToBasketCommand(
                customerId,
                request.ProductId,
                request.ProductName,
                moneyResult.Value,
                request.Quantity);

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Created($"/baskets/{result.Value}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Baskets)
        .WithName("AddItemToBasket")
        .RequireAuthorization(); // Гарантирует доступ только авторизованным пользователям
    }
}
internal sealed record AddItemToBasketRequest(
    Guid ProductId,
    string ProductName,
    decimal Price,
    string Currency,
    int Quantity);