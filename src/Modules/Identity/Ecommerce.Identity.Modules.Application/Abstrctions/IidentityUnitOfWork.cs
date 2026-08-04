

namespace Ecommerce.Identity.Modules.Application.Abstrctions
{
    public interface IidentityUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
