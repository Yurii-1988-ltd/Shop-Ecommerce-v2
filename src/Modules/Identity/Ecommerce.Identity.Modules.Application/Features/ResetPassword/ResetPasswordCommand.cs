
namespace Ecommerce.Identity.Modules.Application.Features.ResetPassword;

public sealed record ResetPasswordCommand(string Email,string Token,string newPassword) : ICommand;

