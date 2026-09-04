namespace Ecommerce.Storefront.ApiClients.Identity.Models;

public record LoginRequest(
    string Email,
    string Password);
