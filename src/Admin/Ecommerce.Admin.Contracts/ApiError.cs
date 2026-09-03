
namespace Ecommerce.Admin.Contracts;

public sealed record ApiError(string Code,
    string Description,
    string Type);            

