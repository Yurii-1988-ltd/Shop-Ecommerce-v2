using Ecommerce.Domain.Domain;
using Ecommerce.Modules.Users.Domain.Errors;
using Ecommerce.Modules.Users.Domain.Events;

namespace Ecommerce.Modules.Users.Domain.Entities;

public sealed class User : Entity
{
    private User()
    {
    }


    public string Email { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; }

    public static Result<User> Create(
     string email,
     string passwordHash,
     string firstName,
     string lastName)
    {
        // Пример доменных проверок
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<User>.Failure(UserErrors.EmailRequired);
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result<User>.Failure(UserErrors.FirstNameRequired);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result<User>.Failure(UserErrors.LastNameRequired);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            CreatedAtUtc = DateTime.UtcNow
        };

        user.AddDomainEvent(new CreatedUserDomainEvent(user.Id));

        return Result<User>.Success(user);
    }

    public  Result UpdateProfile(string firstName, string lastName,string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return UserErrors.FirstNameRequired;
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return UserErrors.LastNameRequired;
        }
        if (string.IsNullOrWhiteSpace(email))
            return UserErrors.EmailRequired;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        PhoneNumber = phoneNumber.Trim();


        AddDomainEvent(new UserUpdatedDomainEvent(Id));
        return Result.Success();
    }
    public  Result ChangePassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return UserErrors.PasswordRequired;
        }
        PasswordHash = passwordHash;
        AddDomainEvent(new UserPasswordChangedDomainEvent(Id));
        return Result.Success();

    }
}