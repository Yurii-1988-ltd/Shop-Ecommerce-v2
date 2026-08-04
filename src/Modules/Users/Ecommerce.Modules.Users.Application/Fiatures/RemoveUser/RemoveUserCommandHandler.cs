using Ecommerce.Modules.Users.Application.Abstractions;
using Ecommerce.Modules.Users.Domain.Errors;

namespace Ecommerce.Modules.Users.Application.Fiatures.RemoveUser
{
    internal sealed class RemoveUserCommandHandler(IUserRepository userRepository, IUserUnitOfWork unitOfWork) : ICommandHandler<RemoveUserCommand>
    {
        public async Task<Result> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return UserErrors.NotFound(request.Id);
            }
            userRepository.Remove(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
