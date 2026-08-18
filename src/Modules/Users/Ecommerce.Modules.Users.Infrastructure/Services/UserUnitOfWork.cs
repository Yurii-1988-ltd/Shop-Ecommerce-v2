



namespace Ecommerce.Modules.Users.Infrastructure.Services
{
    internal class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly UserDbContext _dbContext;

        public UserUnitOfWork(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);
      
    }
}
