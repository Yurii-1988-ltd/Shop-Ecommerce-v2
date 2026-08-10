
using Export.Application.Abstractions;
using Export.Infrastructure.Pdf;
using Microsoft.Extensions.DependencyInjection;

namespace Export.Infrastructure;

public static class ExportModuleExtensions
{
    public static IServiceCollection AddExportModule(this IServiceCollection services)
    {
        services.AddScoped<IExcelExporter, ClosedXmlExporter>();
        services.AddScoped<IPdfExporter, QuestPdfExporter>();
        return services;
    }
}
