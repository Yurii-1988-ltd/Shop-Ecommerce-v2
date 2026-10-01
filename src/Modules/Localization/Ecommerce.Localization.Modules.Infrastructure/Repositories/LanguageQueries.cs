using Dapper;
using Ecommerce.Application.Pagination;
using Ecommerce.Localization.Modules.Application.DTO;

namespace Ecommerce.Localization.Modules.Infrastructure.Queries;

internal sealed class LanguageQueries(
    NpgsqlDataSource dataSource) : ILanguageQueries
{
    public async Task<PagedResult<LanguageResponse>> GetPagedAsync(
     string? cultureCode,
     string? name,
     string? nativeName,
     bool? isDefault,
     int page,
     int pageSize,
     CancellationToken cancellationToken = default)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var offset = (page - 1) * pageSize;

        const string countSql = """
        SELECT COUNT(*)
        FROM "languages"
        WHERE (@CultureCode IS NULL
               OR "CultureCode" ILIKE '%' || @CultureCode || '%')
          AND (@Name IS NULL
               OR "Name" ILIKE '%' || @Name || '%')
          AND (@NativeName IS NULL
               OR "NativeName" ILIKE '%' || @NativeName || '%')
          AND (@IsDefault IS NULL
               OR "IsDefault" = @IsDefault);
        """;

        const string dataSql = """
        SELECT
            "Id",
            "CultureCode",
            "Name",
            "NativeName",
            "IsEnabled",
            "IsDefault"
        FROM "languages"
        WHERE (@CultureCode IS NULL
               OR "CultureCode" ILIKE '%' || @CultureCode || '%')
          AND (@Name IS NULL
               OR "Name" ILIKE '%' || @Name || '%')
          AND (@NativeName IS NULL
               OR "NativeName" ILIKE '%' || @NativeName || '%')
          AND (@IsDefault IS NULL
               OR "IsDefault" = @IsDefault)
        ORDER BY "Name"
        OFFSET @Offset
        LIMIT @PageSize;
        """;

        await using var connection =
            await dataSource.OpenConnectionAsync(cancellationToken);

        var parameters = new
        {
            CultureCode = string.IsNullOrWhiteSpace(cultureCode)
                ? null
                : cultureCode.Trim(),

            Name = string.IsNullOrWhiteSpace(name)
                ? null
                : name.Trim(),

            NativeName = string.IsNullOrWhiteSpace(nativeName)
                ? null
                : nativeName.Trim(),

            IsDefault = isDefault,
            Offset = offset,
            PageSize = pageSize
        };

        var totalCount = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                countSql,
                parameters,
                cancellationToken: cancellationToken));

        var items = (
            await connection.QueryAsync<LanguageResponse>(
                new CommandDefinition(
                    dataSql,
                    parameters,
                    cancellationToken: cancellationToken))
        ).AsList();

        return new PagedResult<LanguageResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }



    public async Task<LanguageResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                "Id",
                "CultureCode",
                "Name",
                "NativeName",
                "IsEnabled",
                "IsDefault"
            FROM "languages"
            WHERE "Id" = @Id;
            """;

        await using var connection = await dataSource.OpenConnectionAsync(
            cancellationToken);

        var command = new CommandDefinition(
            sql,
            new { Id = id },
            cancellationToken: cancellationToken);

        return await connection.QuerySingleOrDefaultAsync<LanguageResponse>(
            command);
    }
}