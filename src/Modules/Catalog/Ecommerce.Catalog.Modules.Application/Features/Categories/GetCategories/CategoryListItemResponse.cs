namespace Ecommerce.Catalog.Modules.Application.Features.Categories.GetCategories;

public sealed record CategoryListItemResponse(Guid Id, string Name,
                                            string Description, bool IsActive);

