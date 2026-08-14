using Ecommerce.Modules.Users.Application.Abstractions; // Или нужный namespace для IUserUnitOfWork
using Ecommerce.Modules.Users.Contracts.Abstractions;

namespace Ecommerce.Modules.Users.Application.Fiatures.CreateRole
{
    internal sealed class CreateRoleCommandHandler(
        IRoleService service,
        IUserUnitOfWork unitOfWork) // <-- Используем IUserUnitOfWork
        : ICommandHandler<CreateRoleCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(
            CreateRoleCommand request,
            CancellationToken cancellationToken)
        {
            var result = await service.CreateAsync(
                request.Name,
                cancellationToken);

            if (result.IsFailure)
            {
                return result;
            }

            // Вызываем SaveChangesAsync на едином UnitOfWork модуля
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}