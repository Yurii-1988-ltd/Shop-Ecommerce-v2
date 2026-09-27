using Ecommerce.Localization.Modules.Domain.Entities;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Application.Abstractions;

public interface ITranslationRepository
{
    Task<Translation?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Translation?> GetByKeyAsync(
        string key,
        CultureInfo culture,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Translation translation,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Translation translation,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Translation translation,
        CancellationToken cancellationToken = default);
}