using Ecommerce.Localization.Modules.Application.Features.Translation.DeleteTranslation;

namespace Ecommerce.Localization.Modules.Presentation.Translations;

internal sealed class DeleteTranslationEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/translations/{id:guid}",
            async (
                Guid id,
                ISender sender) =>
            {
                var result = await sender.Send(
                    new DeleteTranslationCommand(id));

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.NoContent();
            })
            .WithTags(Tags.Translations);
    }
}