
namespace Ecommerce.Modules.Users.Application.Abstractions;

public interface IUserUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
