

namespace Ecommerce.Modules.Users.Contracts.Dto;

public sealed record UserDto(
  Guid Id,
  string FirstName,
  string LastName,
  string Email);
