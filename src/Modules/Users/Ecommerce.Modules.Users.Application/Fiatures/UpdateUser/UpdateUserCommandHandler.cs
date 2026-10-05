

using Ecommerce.Modules.Users.Application.Abstractions;
using Ecommerce.Modules.Users.Domain.Errors;

namespace Ecommerce.Modules.Users.Application.Fiatures.UpdateUser;

internal sealed class UpdateUserCommandHandler(IUserRepository userRepository, IUserUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return UserErrors.NotFound(request.Id);
        }
        user.UpdateProfile(request.FirstName, request.LastName, request.Email, request.PhoneNumber);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
