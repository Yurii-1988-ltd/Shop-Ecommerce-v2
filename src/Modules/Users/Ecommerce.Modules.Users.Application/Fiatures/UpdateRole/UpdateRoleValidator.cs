
using FluentValidation;

namespace Ecommerce.Modules.Users.Application.Fiatures.UpdateRole;

internal sealed class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleValidator()
    {
        RuleFor(x=>x.Id).NotEmpty();
        RuleFor(x=>x.Name).NotEmpty()
            .MaximumLength(15);
    }
}
