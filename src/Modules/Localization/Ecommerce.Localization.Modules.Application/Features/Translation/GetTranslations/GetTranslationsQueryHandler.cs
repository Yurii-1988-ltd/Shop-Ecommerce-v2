using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;
using Ecommerce.Localization.Modules.Application.DTO;
using Ecommerce.Localization.Modules.Application.Features.Translation.GetTranslations;

namespace Ecommerce.Localization.Modules.Application.Features.Translations.GetTranslations;

public sealed class GetTranslationsQueryHandler(
    ILocalizationQueries queries)
    : IQueryHandler<
        GetTranslationsQuery,
        PagedResult<TranslationResponse>>
{
    public async Task<Result<PagedResult<TranslationResponse>>> Handle(
        GetTranslationsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await queries.GetPagedAsync(
            request.Key,
            request.CultureCode,
            request.Module,
            request.Page,
            request.PageSize,
            cancellationToken);

        return Result.Success(result);
    }
}