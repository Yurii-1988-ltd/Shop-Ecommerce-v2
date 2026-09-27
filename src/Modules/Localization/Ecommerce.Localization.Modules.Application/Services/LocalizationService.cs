using Ecommerce.Localization.Abstractions;
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
        CultureInfo culture,
        CancellationToken cancellationToken = default)
    {
        var translation = await queries.GetAsync(
            key,
            culture,
            cancellationToken);

        return translation ?? key;
    }
}