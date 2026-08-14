

using Ecommerce.Domain.Domain;
using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Contracts.Abstractions;

public interface IRoleService
{
    Task<Result<Guid>> CreateAsync(string name,CancellationToken cancellationToken = default);
    Task<Result<RoleResponse?>>GetByIdAsync(Guid id,CancellationToken cancellationToken = default );
    Task<Result<IReadOnlyList<RoleResponse>>>GetAllAsync(CancellationToken cancellationToken = default);
}
