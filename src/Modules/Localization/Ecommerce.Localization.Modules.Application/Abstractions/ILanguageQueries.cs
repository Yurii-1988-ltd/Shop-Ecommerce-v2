using Ecommerce.Application.Pagination;
using Ecommerce.Localization.Modules.Application.DTO;

namespace Ecommerce.Localization.Modules.Application.Abstractions;

public interface ILanguageQueries
{
    Task<LanguageResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<PagedResult<LanguageResponse>> GetPagedAsync(
     string? cultureCode,
     string? name,
     string? nativeName,
     bool? isDefault,
     int page,
     int pageSize,
     CancellationToken cancellationToken = default);
}