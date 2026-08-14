using Ecommerce.Modules.Users.Application.Abstractions;
using Ecommerce.Modules.Users.Application.Fiatures.RemoveRole;
using Ecommerce.Modules.Users.Contracts.Abstractions;

internal sealed class RemoveRoleCommandHandler(
    IRoleService service,
    IUserUnitOfWork unitOfWork)
    : ICommandHandler<RemoveRoleCommand>
{
    public async Task<Result> Handle(
        RemoveRoleCommand request,
        CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(
            request.Id,
            cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}