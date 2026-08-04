
using FluentValidation;

namespace Ecommerce.Modules.Users.Application.Fiatures.UpdateUser;

internal sealed class UpdateUserConnandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserConnandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
