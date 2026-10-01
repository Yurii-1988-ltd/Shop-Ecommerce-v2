using Ecommerce.Application.CQRS;
using Ecommerce.Localization.Modules.Application.DTO;

public sealed record GetLanguageByIdQuery(
    Guid Id) : IQuery<LanguageResponse?>;