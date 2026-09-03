

namespace Ecommerce.Admin.Contracts;

public sealed class ApiException(ApiError error):Exception(error.Description)
{
    public string Code { get; } = error.Code;
    public string Description { get; } = error.Description;
    public string Type { get; } = error.Type;
}
