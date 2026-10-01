
using Ecommerce.Localization.Abstractions.Abstractions;
using Ecommerce.Localization.Modules.Application.Abstractions;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Application.Services;

public sealed class LocalizationService(
    ILocalizationQueries queries)
    : ILocalizationService
{
    public async Task<string> GetAsync(
        string key,
        string module,
        CultureInfo culture,
        CancellationToken cancellationToken = default)
    {
        var value = await queries.GetAsync(
            key,
            module,
            culture,
            cancellationToken);

        if (value is not null)
            return value;

        var defaultCulture = await queries.GetDefaultCultureAsync(
            cancellationToken);

        if (defaultCulture is not null &&
            !string.Equals(
                defaultCulture.Name,
                culture.Name,
                StringComparison.OrdinalIgnoreCase))
        {
            value = await queries.GetAsync(
                key,
                module,
                defaultCulture,
                cancellationToken);

            if (value is not null)
                return value;
        }

        return key;
    }
}