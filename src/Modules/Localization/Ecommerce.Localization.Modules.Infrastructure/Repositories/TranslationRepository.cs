using Ecommerce.Localization.Modules.Application.DTO;
using Ecommerce.Localization.Modules.Domain.Entities;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Infrastructure.Repositories;

internal sealed class TranslationRepository(LocalizationContext context) : ITranslationRepository
{
    public async Task AddAsync(Translation translation, CancellationToken cancellationToken = default)
    {
        await context.Translations.AddAsync(translation, cancellationToken);
    }

    public Task UpdateAsync(
     Translation translation,
     CancellationToken cancellationToken = default)
    {
        context.Translations.Update(translation);

        return Task.CompletedTask;
    }
    public Task DeleteAsync(
    Translation translation,
    CancellationToken cancellationToken = default)
    {
        context.Translations.Remove(translation);

        return Task.CompletedTask;
    }

    public async Task<Translation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Translations
          .FirstOrDefaultAsync(
              x => x.Id == id,
              cancellationToken);
    }

    public Task<Translation?> GetByKeyAsync(
    string key,
    CultureInfo culture,
    CancellationToken cancellationToken = default)
    {
        return context.Translations
            .FirstOrDefaultAsync(
                x =>
                    x.Key == key &&
                    x.CultureCode == culture.Name,
                cancellationToken);
    }
}
