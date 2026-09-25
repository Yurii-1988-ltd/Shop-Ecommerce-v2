using Ecommerce.Domain.Domain;
using Ecommerce.Notification.Modules.Application.Abstractions;
using Ecommerce.Notification.Modules.Application.Models;
using Fluid;
using System.Reflection;

namespace Ecommerce.Notification.Modules.Infrastructure.Templates;

internal sealed class LiquidEmailTemplateRenderer
    : IEmailTemplateRenderer
{
    private static readonly Assembly Assembly =
        typeof(LiquidEmailTemplateRenderer).Assembly;

    private static readonly FluidParser Parser = new();

    private static readonly TemplateOptions TemplateOptions = CreateOptions();

    private static TemplateOptions CreateOptions()
    {
        var options = new TemplateOptions();

        options.MemberAccessStrategy.Register<OrderCreatedEmailModel>();
        options.MemberAccessStrategy.Register<OrderCreatedEmailItemModel>();

        return options;
    }

    public async Task<Result<string>> RenderAsync<TModel>(
        string templateName,
        TModel model,
        CancellationToken cancellationToken = default)
    {
        var resourceName = Assembly
            .GetManifestResourceNames()
            .FirstOrDefault(name =>
                name.EndsWith(
                    $"Templates.Emails.{templateName}.liquid",
                    StringComparison.OrdinalIgnoreCase));

        if (resourceName is null)
        {
            return Error.NotFound(
                "Notification.TemplateNotFound",
                $"Email template '{templateName}' was not found.");
        }

        await using var stream =
            Assembly.GetManifestResourceStream(resourceName);

        if (stream is null)
        {
            return Error.NotFound(
                "Notification.TemplateNotFound",
                $"Email template '{templateName}' could not be loaded.");
        }

        using var reader = new StreamReader(stream);

        var templateSource = await reader.ReadToEndAsync(
            cancellationToken);

        if (!Parser.TryParse(
                templateSource,
                out var template,
                out var errors))
        {
            return Error.Problem(
                "Notification.TemplateParseError",
                string.Join(Environment.NewLine, errors));
        }

        try
        {
            var context = new TemplateContext(
                model,
                TemplateOptions);

            var html = await template.RenderAsync(context);

            return Result.Success(html);
        }
        catch (Exception ex)
        {
            return Error.Problem(
                "Notification.TemplateRenderError",
                ex.Message);
        }
    }
}