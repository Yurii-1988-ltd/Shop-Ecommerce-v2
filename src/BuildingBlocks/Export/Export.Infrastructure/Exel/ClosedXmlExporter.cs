using ClosedXML.Excel;
using Export.Application.Abstractions;
using Export.Application.Models;

public sealed class ClosedXmlExporter : IExcelExporter
{
    public Task<byte[]> ExportToExcelAsync<T>(
        ExportData<T> data,
        CancellationToken cancellationToken = default)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add(data.Title);

        for (var col = 0; col < data.Columns.Count; col++)
        {
            var cell = worksheet.Cell(1, col + 1);

            cell.Value = data.Columns[col].Title;
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#EAEAEA");
        }

        var row = 2;

        foreach (var item in data.Items)
        {
            cancellationToken.ThrowIfCancellationRequested();

            for (var col = 0; col < data.Columns.Count; col++)
            {
                var value = data.Columns[col].ValueSelector(item);

                worksheet.Cell(row, col + 1).Value =
                    value?.ToString() ?? string.Empty;
            }

            row++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return Task.FromResult(stream.ToArray());
    }
}