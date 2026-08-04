using Ecommerce.Application.CQRS;

namespace Ecommerce.Modules.Users.Application.Fiatures.CreateUser;

public sealed record CreateUserCommand(string Email, 
                                        string FirstName, string LastName,string PasswordHash
                                       ) : ICommand<Guid>;

