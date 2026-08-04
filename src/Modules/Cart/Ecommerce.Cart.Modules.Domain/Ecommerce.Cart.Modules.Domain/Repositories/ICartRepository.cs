


namespace Ecommerce.Cart.Modules.Domain.Repositories;

public interface ICartRepository
{
    Task<Entities.Cart?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Entities.Cart?> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default);

    Task InsertAsync(
      Entities.Cart cart,
        CancellationToken cancellationToken = default);

    Task ReplaceAsync(
        Entities.Cart cart,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
    Task UpdateAsync(
        Entities.Cart cart,
        CancellationToken cancellationToken = default);
        Task<(List<Domain.Entities.Cart> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);
    
}