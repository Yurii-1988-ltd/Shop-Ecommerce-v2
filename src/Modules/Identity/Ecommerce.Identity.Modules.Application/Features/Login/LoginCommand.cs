

using Ecommerce.Identity.Modules.Application.Features.Register;

namespace Ecommerce.Identity.Modules.Application.Features.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<AuthenticationResponse>;

