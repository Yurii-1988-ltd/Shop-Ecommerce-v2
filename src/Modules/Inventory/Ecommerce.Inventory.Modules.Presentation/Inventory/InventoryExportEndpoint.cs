using Ecommerce.Inventory.Modules.Application.Features.GetInventoryReport;
using Ecommerce.Inventory.Modules.Application.Responses;
using Export.Application.Abstractions;
using Export.Application.Models;
using MediatR;

namespace Ecommerce.Inventory.Modules.Presentation.Inventory;

internal sealed class InventoryExportEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/inventories/export/excel", async (
            ISender sender,
            IExcelExporter exporter,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetInventoryReportQuery(),
                cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            var data = CreateExportData(result.Value);

            var file = await exporter.ExportToExcelAsync(
                data,
                cancellationToken);

            return Results.File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "inventories.xlsx");
        })
        .WithTags(Tags.Inventory)
        .WithName("ExportInventoriesExcel")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapGet("/inventories/export/pdf", async (
            ISender sender,
            IPdfExporter exporter,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetInventoryReportQuery(),
                cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            var data = CreateExportData(result.Value);

            var file = await exporter.ExportToPdfAsync(
                data,
                cancellationToken);

            return Results.File(
                file,
                "application/pdf",
                "inventories.pdf");
        })
        .WithTags(Tags.Inventory)
        .WithName("ExportInventoriesPdf")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }

    private static ExportData<InventoryReportItem> CreateExportData(
        IReadOnlyList<InventoryReportItem> items)
    {
        return new ExportData<InventoryReportItem>
        {
            Title = "Inventories",

            Columns =
            [
                new ExportColumn<InventoryReportItem>
                {
                    Title = "SKU",
                    ValueSelector = x => x.SKU
                },

                new ExportColumn<InventoryReportItem>
                {
                    Title = "On Hand",
                    ValueSelector = x => x.OnHandQuantity
                },

                new ExportColumn<InventoryReportItem>
                {
                    Title = "Reserved",
                    ValueSelector = x => x.ReservedQuantity
                },

                new ExportColumn<InventoryReportItem>
                {
                    Title = "Available",
                    ValueSelector = x => x.AvailableQuantity
                },

                new ExportColumn<InventoryReportItem>
                {
                    Title = "Minimum",
                    ValueSelector = x => x.MinimumQuantity
                }
            ],

            Items = items
        };
    }
}