
using Export.Application.Models;

namespace Export.Application.Abstractions;

    public interface IExcelExporter
    {
    Task<byte[]> ExportToExcelAsync<T>(ExportData<T> data, CancellationToken cancellationToken = default);

}

