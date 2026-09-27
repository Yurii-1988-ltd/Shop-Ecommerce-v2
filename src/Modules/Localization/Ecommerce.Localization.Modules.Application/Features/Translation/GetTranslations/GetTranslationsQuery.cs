
using Ecommerce.Application.Pagination;
using Ecommerce.Localization.Modules.Application.DTO;

namespace Ecommerce.Localization.Modules.Application.Features.Translation.GetTranslations;

public sealed record GetTranslationsQuery(
    string? Key,
    string? CultureCode,
    string? Module,
    int Page = 1,
    int PageSize = 20)
    : PagedQuery<TranslationResponse>(Page, PageSize);