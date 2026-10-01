using Ecommerce.Catalog.Modules.Application.Features.Products.CreateProduct;
using Ecommerce.Localization.Abstractions.Abstractions;

namespace Ecommerce.Catalog.Modules.Presentation.Products;

internal sealed class CreateProductEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (
            CreateProductRequest request,
            ISender sender,
            ILocalizationService localization,
            ICurrentCulture currentCulture,
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

            if (result.IsSuccess)
                return Results.Created(
                    $"/products/{result.Value}",
                    result.Value);

            var error = result.Error;

            var translationKey =
                ProductErrorLocalization.GetTranslationKey(error.Code);

            if (translationKey is null)
                return Results.BadRequest(error);

            var message = await localization.GetAsync(
                translationKey,
                "Catalog",
                currentCulture.Culture,
                cancellationToken);

            return Results.BadRequest(new
            {
                error.Code,
                Description = message,
                Type = error.Type.ToString()
            });
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
