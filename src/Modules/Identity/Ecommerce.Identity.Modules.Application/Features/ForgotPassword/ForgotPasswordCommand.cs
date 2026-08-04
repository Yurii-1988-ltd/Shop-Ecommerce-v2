

namespace Ecommerce.Identity.Modules.Application.Features.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : ICommand;

