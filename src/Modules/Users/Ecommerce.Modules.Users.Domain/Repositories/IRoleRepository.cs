using Ecommerce.Domain.Domain;
using Ecommerce.Modules.Users.Domain.Entities;

namespace Ecommerce.Modules.Users.Domain.Repositories;

public interface IRoleRepository
{
    void Add(Role role);
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken = default);
    public  Task<Result> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    public void Update(Role role);
    void Remove(Role role);
  
}

