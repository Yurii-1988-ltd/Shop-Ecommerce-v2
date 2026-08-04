
namespace Ecommerce.Modules.Users.Contracts.Requests
{
    public record UserAuthenticationResponse( Guid Id, string Email, string PasswordHash);

}
