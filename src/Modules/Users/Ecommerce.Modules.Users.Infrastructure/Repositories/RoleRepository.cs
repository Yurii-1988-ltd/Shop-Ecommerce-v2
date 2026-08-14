
namespace Ecommerce.Modules.Users.Infrastructure.Repositories;

internal sealed class RoleRepository(UserDbContext context) : IRoleRepository
{
    public void Add(Role role)
    {
        context.Roles.Add(role);
    }

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    => await context.Roles.Where(x =>x.Id == id)
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    =>await context.Roles.AsNoTracking()
        .OrderBy(x => x.Name)
        .ToListAsync(cancellationToken);

    public async Task<Result> RemoveAsync(
      Guid id,
      CancellationToken cancellationToken = default)
    {
        var role = await context.Roles.FindAsync(
            [id],
            cancellationToken);

        if (role is null)
        {
            return RolesErrors.NotFound(id);
        }

        context.Roles.Remove(role);

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Guid Id, string name, CancellationToken cancellationToken = default)
    {
        var roles = await context
            .Roles.FindAsync([Id], cancellationToken);
        if(roles is null)
            return RolesErrors.NotFound(Id);
        context.Roles.Update(roles);
        return Result.Success();

      
    }
}
