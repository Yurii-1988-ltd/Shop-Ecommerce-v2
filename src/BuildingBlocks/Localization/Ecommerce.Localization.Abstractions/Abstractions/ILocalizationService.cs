
using System.Globalization;

namespace Ecommerce.Localization.Abstractions.Abstractions;

public interface ILocalizationService
{
    Task<string> GetAsync(
        string key,
        CultureInfo culture, CancellationToken cancellationToken = default);

}
