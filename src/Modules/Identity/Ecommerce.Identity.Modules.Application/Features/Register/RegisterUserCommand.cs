
using Ecommerce.Application.CQRS;

namespace Ecommerce.Identity.Modules.Application.Features.Register;

public sealed record RegisterUserCommand(string Email, string Password,string FirstName, string LastName):ICommand<AuthenticationResponse>
{
}
