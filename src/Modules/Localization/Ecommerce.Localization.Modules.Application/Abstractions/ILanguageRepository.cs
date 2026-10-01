using Ecommerce.Localization.Modules.Domain.Entities;

namespace Ecommerce.Localization.Modules.Application.Abstractions;

public interface ILanguageRepository
{
    Task<Language?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Language?> GetByCultureCodeAsync(string cultureCode, CancellationToken cancellationToken = default);
    Task AddAsync(Language language, CancellationToken cancellationToken = default);
    Task DeleteAsync(Language language, CancellationToken cancellationToken);
    Task<Language?> GetDefaultAsync(CancellationToken cancellationToken);
}
