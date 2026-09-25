namespace Ecommerce.Notification.Modules.Application.Abstractions;

public interface IEmailTemplateRenderer
{
    Task<string> RenderAsync<TModel>(
        string templateName,
        TModel model,
        CancellationToken cancellationToken = default);
}