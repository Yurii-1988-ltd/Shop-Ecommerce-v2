using Ecommerce.Application.Pagination;
using Ecommerce.Localization.Modules.Application.DTO;

namespace Ecommerce.Localization.Modules.Application.Features.Language.GetLanguages;

public sealed record GetLanguagesQuery(
    string? CultureCode,
    string? Name,
    string? NativeName,
    bool? IsDefault,
    int Page = 1,
    int PageSize = 20)
    : PagedQuery<LanguageResponse>(Page, PageSize);