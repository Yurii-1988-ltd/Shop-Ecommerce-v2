

using Ecommerce.Domain.Domain;
using Ecommerce.Localization.Modules.Domain.Entities;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Application.Abstractions;

public interface ITranslationRepository
{
    Task<Translation?> GetAsync(string key,
                                CultureInfo cultureCode,
                                CancellationToken cancellationToken = default);
    Task AddAsync(Translation translation, CancellationToken cancellationToken = default);
}
