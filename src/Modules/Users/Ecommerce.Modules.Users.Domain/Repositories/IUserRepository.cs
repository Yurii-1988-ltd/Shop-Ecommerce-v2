

using Ecommerce.Modules.Users.Domain.Entities;

namespace Ecommerce.Modules.Users.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    void Insert(User user);
    void Remove(User user);
}
