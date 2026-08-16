namespace Ecommerce.Admin.ApiClients.Users.Contracts;

public sealed record UserRequest(

     string Email,
     string FirstName,
    string LastName,
    string PasswordHash);



