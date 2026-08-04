using Ecommerce.Catalog.Modules.Application.Features.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Catalog.Modules.Presentation.Products;


    internal sealed class CreateProductEndpoint
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (
                CreateProductRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateCatalogCommand(
                    request.Name,
                    request.ProductNumber,
                    request.Description,
                    request.Sku,
                    request.Price,
                    request.Currency);

                var result = await sender.Send(command, cancellationToken);

                return result.IsSuccess
                    ? Results.Created($"/products/{result.Value}", result.Value)
                    : Results.BadRequest(result.Error);
            })
            .WithTags(Tags.Products);
        }
    }



public sealed class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string ProductNumber { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Currency { get; set; } = string.Empty;
}
