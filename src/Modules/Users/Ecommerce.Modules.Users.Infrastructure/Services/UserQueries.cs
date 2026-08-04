

using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Infrastructure.Services;

internal sealed class UserQueries(UserDbContext context) : IUserQueries
{
    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.Where(x=>x.Id ==id ).Select(x=>new UserDto(x.Id,
                                                                                x.FirstName, x.LastName,
                                                                                x.Email)).FirstOrDefaultAsync(cancellationToken);

    }
}
