using Ecommerce.Domain.Domain;

namespace Ecommerce.Notification.Modules.Application.Abstractions;

public interface IEmailTemplateRenderer
{
    Task<Result<string>> RenderAsync<TModel>(
        string templateName,
        TModel model,
        CancellationToken cancellationToken = default);
}