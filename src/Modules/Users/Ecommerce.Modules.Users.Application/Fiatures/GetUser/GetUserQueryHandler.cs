using Dapper;
using Ecommerce.Application.Pagination;
using Ecommerce.Modules.Users.Application.Fiatures.GetUser;
using Ecommerce.Modules.Users.Application.Fiatures.GetUsers;
using Ecommerce.Modules.Users.Domain.Errors;

public sealed class GetUsersQueryHandler(
    IDbConnectionFactory dbConnection)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection =
              await dbConnection.OpenConnectionAsync();

        const string sql = $"""
            SELECT
                Id AS {nameof(UserResponse.Id)},
                Email AS {nameof(UserResponse.Email)},
                FirstName AS {nameof(UserResponse.FirstName)},
                LastName AS {nameof(UserResponse.LastName)},
                CreatedAtUtc AS {nameof(UserResponse.CreatedAtUtc)}
            FROM users
            WHERE Id = @Id
            """;

        var user = await connection.QuerySingleOrDefaultAsync<UserResponse>(
            sql,
            new { request.Id });

        if (user is null)
        {
            return UserErrors.NotFound(request.Id);
        }

        return user;
    }
}