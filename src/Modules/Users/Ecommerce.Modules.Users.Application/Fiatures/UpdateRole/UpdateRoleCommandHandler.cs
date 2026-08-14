
using Ecommerce.Modules.Users.Application.Abstractions;
using Ecommerce.Modules.Users.Contracts.Abstractions;

namespace Ecommerce.Modules.Users.Application.Fiatures.UpdateRole;

internal sealed class UpdateRoleCommandHandler(IRoleService roleService, IUserUnitOfWork unitOfWork) : ICommandHandler<UpdateRoleCommand>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await roleService.UpdateAsync(request.Id, request.Name, cancellationToken);
        if (result.IsFailure)
            return result;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;


        
    }
}
