

namespace Ecommerce.Identity.Modules.Application.Abstrctions;

public interface ITokenProvider
{
    string GenerateRefreshToken();
}
