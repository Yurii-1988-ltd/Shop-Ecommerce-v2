using Dapper;
using Ecommerce.Application.Pagination;
using Ecommerce.Modules.Users.Application.Fiatures.GetUsers;

public sealed class GetUsersQueryHandler(
    IDbConnectionFactory dbConnection)
    : IQueryHandler<GetUsersQuery, PagedResult<UserResponse>>
{
    public async Task<Result<PagedResult<UserResponse>>> Handle(
       GetUsersQuery request,
       CancellationToken cancellationToken)
    {
        using IDbConnection connection =
            await dbConnection.OpenConnectionAsync();

        const string sql = $"""
            SELECT
                e.Id AS {nameof(UserResponse.Id)},
                e.Email AS {nameof(UserResponse.Email)},
                e.FirstName AS {nameof(UserResponse.FirstName)},
                e.LastName AS {nameof(UserResponse.LastName)},
                e.CreatedAtUtc AS {nameof(UserResponse.CreatedAtUtc)}
            FROM users e
            ORDER BY e.CreatedAtUtc DESC
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY;

            SELECT COUNT(*)
            FROM users;
            """;

        var offset = (request.Page - 1) * request.PageSize;

        using var multi = await connection.QueryMultipleAsync(
            sql,
            new
            {
                Offset = offset,
                request.PageSize
            });

        var users = (await multi.ReadAsync<UserResponse>())
            .ToList();

        var totalCount = await multi.ReadSingleAsync<int>();

        var result = new PagedResult<UserResponse>
        {
            Items = users,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };

        return result;
    }
}