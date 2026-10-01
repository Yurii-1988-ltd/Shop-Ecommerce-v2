using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Features.Language.CreateLanguage;


namespace Ecommerce.Localization.Modules.Presentation.Localizations;

internal sealed class CreateLanguageEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/languages",
            async (
                CreateLanguageCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    command,
                    cancellationToken);

                if (result.IsFailure)
                    return Results.Problem(
                        statusCode: result.Error.Type switch
                        {
                            ErrorType.Conflict => StatusCodes.Status409Conflict,
                            ErrorType.Validation => StatusCodes.Status400BadRequest,
                            ErrorType.NotFound => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        },
                        title: result.Error.Code,
                        detail: result.Error.Description);

                return Results.Created(
                    $"/languages/{result.Value}",
                    result.Value);
            })
            .WithTags(Tags.Languages);
    }
}