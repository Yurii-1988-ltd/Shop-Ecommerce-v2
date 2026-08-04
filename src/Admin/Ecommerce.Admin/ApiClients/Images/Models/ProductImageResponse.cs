namespace Ecommerce.Admin.ApiClients.Images.Models;

public sealed record ProductImageResponse
{
    public Guid Id { get; init; }

    public string Url { get; init; } = string.Empty;

    public string StorageKey { get; init; } = string.Empty;

    public string AltText { get; init; } = string.Empty;

    public bool IsPrimary { get; init; }

    public int DisplayOrder { get; init; }
}