
namespace Ecommerce.Modules.Users.Application.Fiatures.GetUsers
{
    public sealed class GetUsersQueryHandler(IDbConnectionFactory dbConnection) : IQueryHandler<GetUsersQuery, IReadOnlyList<UserResponse>>
    {
        public async  Task<Result<IReadOnlyList<UserResponse>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            using IDbConnection connection =
            await dbConnection.OpenConnectionAsync();
            const string sql =
                $"""
                   
                         SELECT 
                           Id AS {nameof(UserResponse.Id)},
                           Email AS {nameof(UserResponse.Email)},
                          FirstName AS {nameof(UserResponse.FirstName)},
                           LastName AS {nameof(UserResponse.LastName)},
                           CreatedAtUtc AS {nameof(UserResponse.CreatedAtUtc)}
                        FROM users 
                    
                    """;
            List<UserResponse>users =( await connection.QueryAsync<UserResponse>(sql,query)).AsList();
            return users;
        }
    }
}
