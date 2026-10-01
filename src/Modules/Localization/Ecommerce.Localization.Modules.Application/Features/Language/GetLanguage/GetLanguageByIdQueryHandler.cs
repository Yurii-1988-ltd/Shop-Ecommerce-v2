using Ecommerce.Application.CQRS;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;
using Ecommerce.Localization.Modules.Application.DTO;

internal sealed class GetLanguageByIdQueryHandler(
    ILanguageQueries queries)
    : IQueryHandler<GetLanguageByIdQuery, LanguageResponse?>
{
    public async Task<Result<LanguageResponse?>> Handle(
        GetLanguageByIdQuery request,
        CancellationToken cancellationToken)
    {
        var language = await queries.GetByIdAsync(
            request.Id,
            cancellationToken);

        return Result.Success(language);
    }
}