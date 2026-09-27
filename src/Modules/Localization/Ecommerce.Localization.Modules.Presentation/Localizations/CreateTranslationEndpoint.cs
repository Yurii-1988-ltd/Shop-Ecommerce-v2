using Ecommerce.Localization.Modules.Application.Features.Translation.CreateTranslation;

namespace Ecommerce.Localization.Modules.Presentation.Translations;

internal sealed class CreateTranslationEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/translations",
            async (
                CreateTranslationCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    command,
                    cancellationToken);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            })
            .WithTags(Tags.Translations);
    }
}