using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Application.Abstractions;
using Ecommerce.Localization.Modules.Application.DTO;

namespace Ecommerce.Localization.Modules.Application.Features.Language.GetLanguages;

internal sealed class GetLanguagesQueryHandler(
    ILanguageQueries queries)
    : IQueryHandler<GetLanguagesQuery, PagedResult<LanguageResponse>>
{
    public async Task<Result<PagedResult<LanguageResponse>>> Handle(
        GetLanguagesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await queries.GetPagedAsync(
            request.CultureCode,
            request.Name,
            request.NativeName,
            request.IsDefault,
            request.Page,
            request.PageSize,
            cancellationToken);

        return Result.Success(result);
    }
}