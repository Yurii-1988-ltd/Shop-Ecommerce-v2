

using FluentValidation;

namespace Ecommerce.Modules.Users.Application.AssignRole;

internal sealed class AssignRoleValidator: AbstractValidator<AssignRoleCommand>
{
    public AssignRoleValidator()
    {
        RuleFor(x=>x.UserId).NotEmpty();
        RuleFor(x=>x.RoleId).NotEmpty();
    }
}
