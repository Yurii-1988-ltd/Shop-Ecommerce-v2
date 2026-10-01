

using System.Globalization;

namespace Ecommerce.Localization.Modules.Infrastructure.Services
{
    internal sealed class CurrentCulture : ICurrentCulture
    {
        public CultureInfo Culture => CultureInfo.CurrentCulture;
    }
}
