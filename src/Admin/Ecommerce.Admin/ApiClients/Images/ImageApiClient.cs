using Ecommerce.Admin.ApiClients.Images.Contracts;
using Ecommerce.Admin.ApiClients.Images.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Ecommerce.Admin.ApiClients.Images;

internal sealed class ImageApiClient(HttpClient httpClient) : IImageApiClient
{
    public async Task ChangeImageOrderAsync(
        Guid productId,
        Guid imageId,
        int newIndex,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(
            $"/products/{productId}/images/{imageId}/order",
            new ChangeImageOrderRequest(newIndex),
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task AddProductImageAsync(Guid productId, AddProductImageRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync($"/products/{productId}/images",
            request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveProductImageAsync(Guid productId, Guid imageId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/products/{productId}/images/{imageId}",
            cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task SetPrimaryImageAsync(Guid productId, Guid imageId, CancellationToken cancellationToken = default)
    {
        // Добавляем "/primary" в конец URL, чтобы он полностью совпал с бэкендом
        var response = await httpClient.PutAsync($"/products/{productId}/images/{imageId}/primary",
            content: null, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<UploadImageResponse> UploadImageAsync(IBrowserFile file, CancellationToken cancellationToken = default)
    {
        using var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024);
        using var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(stream);
        streamContent.Headers.ContentType = 
            new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
        content.Add(streamContent, "file", file.Name);

        var response = await httpClient.PostAsync("/images", content, cancellationToken);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<UploadImageResponse>(cancellationToken))!;
    }
}