using Export.Application.Abstractions;
using Export.Application.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Export.Infrastructure.Pdf;

public sealed class QuestPdfExporter : IPdfExporter
{
    public  Task<byte[]> ExportToPdfAsync<T>(
        ExportData<T> data,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header()
                    .Text(data.Title)
                    .SemiBold()
                    .FontSize(20)
                    .FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            foreach (var _ in data.Columns)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        // Header
                        table.Header(header =>
                        {
                            foreach (var column in data.Columns)
                            {
                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(5)
                                    .Text(column.Title)
                                    .SemiBold();
                            }
                        });

                        // Data
                        foreach (var item in data.Items)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            foreach (var column in data.Columns)
                            {
                                var value = column.ValueSelector(item);

                                table.Cell()
                                    .Padding(5)
                                    .Text(value?.ToString() ?? string.Empty);
                            }
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Page ");
                        text.CurrentPageNumber();
                    });
            });
        });

        return  Task.FromResult(document.GeneratePdf());
    }
}