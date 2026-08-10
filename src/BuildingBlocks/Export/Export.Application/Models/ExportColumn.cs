namespace Export.Application.Models;

public sealed class ExportColumn<T>
{
    public string Title { get; init; } = string.Empty;

    public Func<T, object?> ValueSelector { get; init; } = default!;
}

public sealed class ExportData<T>
{
    public const string DefaultTitle = "Report";

    public string Title { get; init; } = DefaultTitle;

    public IReadOnlyList<ExportColumn<T>> Columns { get; init; }
        = [];

    public IEnumerable<T> Items { get; init; }
        = Enumerable.Empty<T>();
}