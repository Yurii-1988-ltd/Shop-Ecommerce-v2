using Ecommerce.Modules.Users.Application.Abstractions;
using Ecommerce.Modules.Users.Application.Fiatures.CreateUser;
using Ecommerce.Modules.Users.Contracts.Abstractions;
using Ecommerce.Modules.Users.Contracts.Requests;

public sealed class CreateUserCommandHandler(IUserService userService,IUserUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
         var result = await userService.CreateAsync(
            new CreateUserRequest(
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName),
            cancellationToken);
        if (result.IsFailure)
        {
            return result;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}