namespace Ecommerce.Storefront.ApiClients.Identity.Models;

public record AuthenticationResponse(
    string AccessToken,
    string RefreshToken);
