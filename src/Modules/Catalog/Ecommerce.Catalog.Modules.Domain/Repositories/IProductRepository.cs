

using Ecommerce.Catalog.Modules.Domain.Entities;

namespace Ecommerce.Catalog.Modules.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task InsertAsync(Product product,CancellationToken cancellationToken );
    Task ReplaceAsync(Product product, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken);
    Task<bool> ExistOrderNumberAsync(string productNumber, CancellationToken cancellationToken = default);
}
