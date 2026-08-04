

using FluentValidation;

namespace Ecommerce.Identity.Modules.Application.Features.Register;

internal sealed class RegisterValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
    .NotEmpty()
    .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);
    }

}
