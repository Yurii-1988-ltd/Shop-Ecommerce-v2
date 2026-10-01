

namespace Ecommerce.Localization.Modules.Presentation.Localizations;

internal sealed class GetLanguageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/languages/{id:guid}",
            async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetLanguageByIdQuery(id),
                    cancellationToken);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);
                if(result.Value is null)
                    return Results.NotFound();
                return Results.Ok(result.Value);
            })
            .WithTags(Tags.Languages);
    }
}
