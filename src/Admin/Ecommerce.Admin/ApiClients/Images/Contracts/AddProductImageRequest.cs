namespace Ecommerce.Admin.ApiClients.Images.Contracts;

public sealed record AddProductImageRequest(
    string StorageKey,
    string AltText);

