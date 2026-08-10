
using Export.Application.Models;

namespace Export.Application.Abstractions;

public interface IPdfExporter
{
    Task<byte[]> ExportToPdfAsync<T>(
        ExportData<T> data,
        CancellationToken cancellationToken = default);
}
