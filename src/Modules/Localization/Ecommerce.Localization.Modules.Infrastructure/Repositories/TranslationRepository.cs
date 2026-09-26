
using Ecommerce.Localization.Modules.Application.Abstractions;
using Ecommerce.Localization.Modules.Domain.Entities;

using System.Globalization;

namespace Ecommerce.Localization.Modules.Infrastructure.Repositories;

internal sealed class TranslationRepository(LocalizationContext context) : ITranslationRepository
{
    public async Task AddAsync(Translation translation, CancellationToken cancellationToken = default)
    {
        await context.Translations.AddAsync(translation, cancellationToken);
    }

    public async Task<Translation?> GetAsync(string key, CultureInfo cultureCode, CancellationToken cancellationToken = default)
    {
       return await context.Translations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Key == key &&
            x.CultureCode == cultureCode.Name, cancellationToken);
    }
}
