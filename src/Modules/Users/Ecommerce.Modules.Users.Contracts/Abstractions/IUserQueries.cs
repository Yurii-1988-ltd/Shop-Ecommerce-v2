

using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Contracts.Abstractions
{
    public interface IUserQueries
    {
        Task<UserDto?> GetByIdAsync(
       Guid id,
       CancellationToken cancellationToken = default);
    }
}
