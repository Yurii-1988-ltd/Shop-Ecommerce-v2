
using System.Globalization;

namespace Ecommerce.Localization.Abstractions.Abstractions;

public interface ILocalizationService
{
    Task<string> GetAsync(
        string key,
          string module,
        CultureInfo culture, CancellationToken cancellationToken = default);

}
