

using Ecommerce.Domain.Domain;

namespace Ecommerce.Localization.Modules.Domain.Entities;

public sealed class Translation : Entity
{
    #region Properties and constructors
    public string Key { get; private set; } = default!;
    public string CultureCode { get; private set; } = default!;
    public string Value { get; private set; } = default!;
    public string Module { get; private set; } = default!;
    public string? Description { get; private set; }

    private Translation()
    {

    }
    public Translation(Guid id,
        string key,
        string cultureCode,
        string value,
        string module,
        string? description)
    {
        Id = id;
        Key = key;
        CultureCode = cultureCode;
        Value = value;
        Module = module;
        Description = description;
    }
    #endregion
    #region static factory methods
    public static Result<Translation> Create(string key,
        string cultureCode,
        string value,
        string module,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return LocalizationErrors.TranslationKeyRequired;
        }
        var cultureResult = Domain.ValueObject.CultureCode.Create(cultureCode);
        if (cultureResult.IsFailure)
            return cultureResult.Error;
        if (string.IsNullOrWhiteSpace(value))
            return LocalizationErrors.TranslationValueRequired;
        if (string.IsNullOrWhiteSpace(module))
            return LocalizationErrors.ModuleIsRequired;
        return Result.Success(new Translation(
            Guid.NewGuid(),
            key.Trim(),
            cultureResult.Value.Value,
            value,
            module.Trim(),
            description?.Trim()));

    }
    public void Update(string value, string? description)
    {
        Value = value;
        Description = description?.Trim();

    }
    #endregion

}
