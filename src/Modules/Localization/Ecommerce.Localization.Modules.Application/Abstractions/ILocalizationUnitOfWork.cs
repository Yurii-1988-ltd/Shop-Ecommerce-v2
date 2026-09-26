

namespace Ecommerce.Localization.Modules.Application.Abstractions;

public interface ILocalizationUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
