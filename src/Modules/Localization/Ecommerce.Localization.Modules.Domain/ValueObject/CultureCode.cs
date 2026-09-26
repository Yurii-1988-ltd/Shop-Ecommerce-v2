

using Ecommerce.Domain.Domain;
using System.Globalization;

namespace Ecommerce.Localization.Modules.Domain.ValueObject;

public sealed record CultureCode
{
    public string Value { get; }
    private CultureCode(string value)
    {
        Value = value;
    }
    public static Result<CultureCode>Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return LocalizationErrors.CultureCodeRequired;
       
        try
        {
            var culture = CultureInfo.GetCultureInfo(value);
            return Result.Success(new CultureCode(culture.Name));
        }
        catch (CultureNotFoundException)
        {
            return LocalizationErrors.InvalidCultureCode(value);
        }
    }
    public override string ToString()
     => Value;
}

