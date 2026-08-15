

using Ecommerce.Domain.Domain;

namespace Ecommerce.Modules.Users.Domain.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid userId)
        => new Error(
            "Event.NotFound",
            $"Event with id {userId} not found",
            ErrorType.NotFound);
    public static Error EmailRequired => new Error(
        "User.EmailRequired",
        "Email is required",
        ErrorType.Validation);
    public static Error FirstNameRequired => new Error(
        "User.FirstNameRequired",
        "First name is required",
        ErrorType.Validation);
    public static Error LastNameRequired => new Error(
        "User.LastNameRequired",
        "Last name is required",
        ErrorType.Validation);
    public static Error EmailAlreadyExists(string email)
        => new Error(
            "User.EmailAlreadyExists",
            $"Email '{email}' already exists",
            ErrorType.Conflict);
    public static Error PasswordRequired
         => new Error(
             "User.PasswordRequired",
             "Password is required",
             ErrorType.Validation);
    public static Error RoleIsRequired
        => new Error("Role.IsRequired", "Role is required field", ErrorType.Validation);
    public static Error RoleAssignmentNotFound
        => new Error("Role.Assignment.Not.Found", "Assigment role not found", ErrorType.Validation);
    public static Error RoleAlreadyAssigned
        => new Error("Role.Already.Assigned", "The role is already assigment", ErrorType.Validation);
}
