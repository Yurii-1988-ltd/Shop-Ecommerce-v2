
using Ecommerce.Modules.Users.Application.Abstractions;

namespace Ecommerce.Modules.Users.Application.Fiatures.RemoveUserRole
{
    internal sealed class RemoveUserRoleCommandHandler(IUserRoleRepository userRoleRepository,IUserUnitOfWork unitOfWork) : ICommandHandler<RemoveUserRoleCommand>
    {
        public async Task<Result> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await userRoleRepository.RemoveAsync(request.userId, request.roleId, cancellationToken);
            if(result.IsFailure)
                return result;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
