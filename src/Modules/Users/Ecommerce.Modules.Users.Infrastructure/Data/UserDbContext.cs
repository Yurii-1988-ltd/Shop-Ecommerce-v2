using Ecommerce.Modules.Users.Domain.Entities;
using Ecommerce.Modules.Users.Infrastructure.Configuration;

namespace Ecommerce.Modules.Users.Infrastructure.Data;

public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
    {
        
    }
    public DbSet<User> Users =>Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
