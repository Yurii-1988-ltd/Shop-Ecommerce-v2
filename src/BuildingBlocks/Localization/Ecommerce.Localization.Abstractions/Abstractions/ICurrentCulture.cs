

using System.Globalization;

namespace Ecommerce.Localization.Abstractions.Abstractions;

public interface ICurrentCulture
{
    CultureInfo Culture { get; }
}
