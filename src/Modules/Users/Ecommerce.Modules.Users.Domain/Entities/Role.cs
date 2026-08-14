
using Ecommerce.Domain.Domain;
using Ecommerce.Modules.Users.Domain.Errors;

namespace Ecommerce.Modules.Users.Domain.Entities;

public sealed class Role : Entity
{
 
    public string Name { get; private set; } = string.Empty;
    private Role()
    {
        
    }
    public static Result<Role>Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            return RolesErrors.NameRequired;
        }
        return new Role
        {
            Name = name.Trim(),
        };

    }
}
