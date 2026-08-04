

using FluentValidation;

namespace Ecommerce.Modules.Users.Application.Fiatures.RemoveUser;

internal sealed class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    public RemoveUserCommandValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty();
    }
}
