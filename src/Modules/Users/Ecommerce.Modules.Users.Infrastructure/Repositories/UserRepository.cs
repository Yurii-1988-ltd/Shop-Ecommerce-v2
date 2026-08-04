

using Ecommerce.Modules.Users.Domain.Entities;


namespace Ecommerce.Modules.Users.Infrastructure.Repositories
{
    internal sealed class UserRepository(UserDbContext dbContext) : IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users.SingleOrDefaultAsync(x  => x.Email == email,cancellationToken);
            
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users.SingleOrDefaultAsync(x=>x.Id == id,cancellationToken);
        }

        public void Insert(User user)
        {
           dbContext.Add(user);
        }

        public void Remove(User user)
        {
           dbContext.Remove(user);
        }
    }
}
