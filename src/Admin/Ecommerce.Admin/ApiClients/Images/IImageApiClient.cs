using Ecommerce.Admin.ApiClients.Images.Contracts;
using Ecommerce.Admin.ApiClients.Images.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Ecommerce.Admin.ApiClients.Images
{
    public interface IImageApiClient
    {
        Task<UploadImageResponse> UploadImageAsync(IBrowserFile file,CancellationToken cancellationToken = default);
        Task RemoveProductImageAsync(
       Guid productId,
       Guid imageId,
       CancellationToken cancellationToken = default);
        Task SetPrimaryImageAsync(
      Guid productId,
      Guid imageId,
      CancellationToken cancellationToken = default);

        Task ChangeImageOrderAsync(
            Guid productId,
            Guid imageId,
            int newIndex,
            CancellationToken cancellationToken = default);
        Task AddProductImageAsync(
            Guid productId,
            AddProductImageRequest request,
          
            CancellationToken cancellationToken = default);
    }
}
