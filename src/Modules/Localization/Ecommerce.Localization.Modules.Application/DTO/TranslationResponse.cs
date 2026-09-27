

namespace Ecommerce.Localization.Modules.Application.DTO;

public record TranslationResponse(Guid Id,
    string Key,
    string CultureCode,
    string Value,
    string Module,
    string? Description);

