

using Ecommerce.Basket.Modules.Domain.Entities;
using Ecommerce.Domain.Domain;

namespace Ecommerce.Basket.Modules.Domain.Repository;

public interface IBasketRepository
{
  Task<Result<Domain.Entities.Basket>> GetByCustomerIdAsync(
    Guid customerId,
    CancellationToken cancellationToken = default);
    Task AddAsync(Entities.Basket basket, CancellationToken cancellationToken = default);
     Task<Result> UpdateAsync(Domain.Entities.Basket basket, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);


}
