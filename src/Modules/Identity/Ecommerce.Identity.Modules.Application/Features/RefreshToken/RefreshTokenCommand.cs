
using Ecommerce.Identity.Modules.Application.Dto;

namespace Ecommerce.Identity.Modules.Application.Features.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken): ICommand<AuthenticationResponse>;

