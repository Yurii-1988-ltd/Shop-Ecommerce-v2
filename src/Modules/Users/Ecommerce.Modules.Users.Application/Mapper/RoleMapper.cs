using Ecommerce.Modules.Users.Contracts.Dto;
using Ecommerce.Modules.Users.Domain.Entities;

namespace Ecommerce.Modules.Users.Application.Mapper;

public static class RoleMapper
{
    public static RoleResponse ToResponse(this Role role)
    {
        return new RoleResponse(role.Id, role.Name);
        
            
        
    }
}

