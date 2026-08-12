using Dapper;
using Ecommerce.Inventory.Modules.Application.Abstractions;
using Ecommerce.Inventory.Modules.Application.Responses;
using Npgsql;

namespace Ecommerce.Inventory.Modules.Infrastructure.Database.Queries;

internal sealed class InventoryQueries(
    NpgsqlDataSource dataSource)
    : IInventoryQueries
{
    public async Task<IReadOnlyList<InventoryReportItem>> GetReportAsync(
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                "Id" AS "InventoryItemId",
                "ProductId",
                "SKU",
                "OnHandQuantity",
                "ReservedQuantity",
                ("OnHandQuantity" - "ReservedQuantity") AS "AvailableQuantity",
                "MinimumQuantity"
            FROM "inventories"
            ORDER BY "SKU";
            """;

        await using var connection =
            await dataSource.OpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(
            sql,
            cancellationToken: cancellationToken);

        var result =
            await connection.QueryAsync<InventoryReportItem>(command);

        return result.AsList();
    }
}