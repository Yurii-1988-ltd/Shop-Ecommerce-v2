

using FluentValidation;

namespace Ecommerce.Identity.Modules.Application.Features.Login
{
    internal class LoginValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
  
}
