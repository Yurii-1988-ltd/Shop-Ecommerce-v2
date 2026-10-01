

using Ecommerce.Application.Pagination;
using Ecommerce.Localization.Modules.Application.DTO;
using Ecommerce.Localization.Modules.Domain.Entities;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Application.Abstractions;

public interface ILocalizationQueries
{
    Task<string?> GetAsync(
      string key,
       string module,
      CultureInfo culture,
     
      CancellationToken cancellationToken = default);
    Task<TranslationResponse?> GetByIdAsync(
Guid id,
CancellationToken cancellationToken = default);
    Task<PagedResult<TranslationResponse>> GetPagedAsync(
                        string Key,
                        string? cultureCode,
                        string? module,
                        int page,
                        int pageSize,
                        CancellationToken cancellationToken = default);
    Task<CultureInfo?> GetDefaultCultureAsync(
    CancellationToken cancellationToken = default);

}
