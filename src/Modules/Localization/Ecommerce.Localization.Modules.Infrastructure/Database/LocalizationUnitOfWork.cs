
namespace Ecommerce.Localization.Modules.Infrastructure.Database;

internal sealed class LocalizationUnitOfWork(LocalizationContext context): ILocalizationUnitOfWork
{
    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
