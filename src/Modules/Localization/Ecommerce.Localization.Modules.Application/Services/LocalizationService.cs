

using Ecommerce.Localization.Abstractions.Abstractions;
using Ecommerce.Localization.Modules.Application.Abstractions;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Application.Services;

public sealed class LocalizationService(ITranslationRepository repository) : ILocalizationService
{
    public async   Task<string> GetAsync(
        string key,
        CultureInfo cultureCode, CancellationToken cancellationToken = default)
        {
             var transaction = await repository.GetAsync(key, cultureCode, cancellationToken);
             return transaction?.Value ?? key;

        }


}

            