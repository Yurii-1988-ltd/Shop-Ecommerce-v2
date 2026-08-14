
using Ecommerce.Domain.Domain;

namespace Ecommerce.Modules.Users.Domain.Errors;

public static class RolesErrors
{
    public static Error NotFound(Guid id)
        =>new Error("Roles.NotFound","The role is not found",ErrorType.NotFound);
    public static Error NameRequired
        => new Error("Name.Required", " The name field is required", ErrorType.Validation);

}

