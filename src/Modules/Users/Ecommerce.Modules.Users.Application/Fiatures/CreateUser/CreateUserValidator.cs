using FluentValidation;

namespace Ecommerce.Modules.Users.Application.Fiatures.CreateUser;

internal sealed class CreateUserValidator : AbstractValidator<User>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty()
            .WithMessage("First name is required field");
        RuleFor(x => x.LastName).NotEmpty()
          .WithMessage("Last name is required field");
        RuleFor(x => x.Email).NotEmpty()
            .EmailAddress();
    }
}
