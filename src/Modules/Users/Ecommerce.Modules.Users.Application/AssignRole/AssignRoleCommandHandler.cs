using Ecommerce.Modules.Users.Application.Abstractions;
using Ecommerce.Modules.Users.Application.AssignRole;
using Ecommerce.Modules.Users.Contracts.Abstractions;

internal sealed class AssignRoleCommandHandler(
    IUserRoleService userRoleService,
    IUserUnitOfWork unitOfWork)
    : ICommandHandler<AssignRoleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        AssignRoleCommand request,
        CancellationToken cancellationToken)
    {
        var result = await userRoleService.AssignAsync(
            request.UserId,
            request.RoleId,
            cancellationToken);

        if (result.IsFailure)
        {
            return result.Error;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value;
    }
}