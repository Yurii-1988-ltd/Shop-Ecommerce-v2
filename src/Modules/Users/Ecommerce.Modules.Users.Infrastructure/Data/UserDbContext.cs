using Ecommerce.Modules.Users.Domain.Entities;
using Ecommerce.Modules.Users.Infrastructure.Configuration;

namespace Ecommerce.Modules.Users.Infrastructure.Data;

public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
    {
        
    }
    public DbSet<User> Users =>Set<User>();
    public DbSet<Role> Roles =>Set<Role>();
    public DbSet<UserRole> UserRoles =>Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new  RoleConfiguration());
    }
}
