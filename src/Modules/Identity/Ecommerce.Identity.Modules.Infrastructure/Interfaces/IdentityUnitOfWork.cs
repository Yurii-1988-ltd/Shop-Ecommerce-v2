

namespace Ecommerce.Identity.Modules.Infrastructure.Interfaces;

internal sealed class IdentityUnitOfWork : IidentityUnitOfWork
{
    private readonly IdentityContext _identityContext;

    public IdentityUnitOfWork(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _identityContext.SaveChangesAsync(cancellationToken);
       
}
