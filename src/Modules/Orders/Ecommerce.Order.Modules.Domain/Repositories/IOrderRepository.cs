
using Ecommerce.Order.Modules.Domain.Entities;

namespace Ecommerce.Order.Modules.Domain.Repositories;

public interface IOrderRepository
{
    Task InsertAsync(Entities.Order order,CancellationToken cancellationToken = default);
   Task<Result> UpdateAsync(Entities.Order order, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Entities.Order?>GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<(List<Domain.Entities.Order> Items, int TotalCount)> GetPagedAsync(
    int page,
    int pageSize,
    CancellationToken cancellationToken = default);
    Task<bool>ExistOrderNumberAsync(string orderNumber, CancellationToken cancellationToken = default);
}
