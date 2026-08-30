namespace Ecommerce.Admin.ApiClients.Identity.Contracts;

internal record RegisterRequest(string Email, string Password, string FirstName, string LastName);