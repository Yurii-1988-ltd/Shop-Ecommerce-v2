
namespace Ecommerce.Localization.Modules.Application.DTO;

public record LanguageResponse(Guid Id,
                 string CultureCode,
                 string Name,
                 string NativeName,
                 bool IsEnabled,
                  bool IsDefault);

