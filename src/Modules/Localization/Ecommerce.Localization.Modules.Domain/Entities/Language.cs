using Ecommerce.Domain.Domain;

namespace Ecommerce.Localization.Modules.Domain.Entities;

public sealed class Language : Entity
{
    #region Properties and Constructor

    public string CultureCode { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string NativeName { get; private set; } = default!;
    public bool IsEnabled { get; private set; }
    public bool IsDefault { get; private set; }

    private Language()
    {
    }

    private Language(
        Guid id,
        string cultureCode,
        string name,
        string nativeName,
        bool isDefault)
    {
        Id = id;
        CultureCode = cultureCode;
        Name = name;
        NativeName = nativeName;
        IsEnabled = true;
        IsDefault = isDefault;
    }

    #endregion

    #region Factory

    public static Result<Language> Create(
        string cultureCode,
        string name,
        string nativeName,
        bool isDefault = false)
    {
        var cultureResult = ValueObject.CultureCode.Create(cultureCode);

        if (cultureResult.IsFailure)
            return cultureResult.Error;

        if (string.IsNullOrWhiteSpace(name))
            return LocalizationErrors.LanguageNameRequired;

        if (string.IsNullOrWhiteSpace(nativeName))
            return LocalizationErrors.LanguageNativeNameRequired;

        return Result.Success(
            new Language(
                Guid.NewGuid(),
                cultureResult.Value.Value,
                name.Trim(),
                nativeName.Trim(),
                isDefault));
    }

    #endregion

    #region State

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void SetAsDefault()
    {
        IsDefault = true;
    }

    public void RemoveAsDefault()
    {
        IsDefault = false;
    }

    #endregion

    #region Changes

    public Result ChangeCultureCode(string cultureCode)
    {
        var cultureResult = ValueObject.CultureCode.Create(cultureCode);

        if (cultureResult.IsFailure)
            return cultureResult.Error;

        CultureCode = cultureResult.Value.Value;

        return Result.Success();
    }

    public Result ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return LocalizationErrors.LanguageNameRequired;

        Name = name.Trim();

        return Result.Success();
    }

    public Result ChangeNativeName(string nativeName)
    {
        if (string.IsNullOrWhiteSpace(nativeName))
            return LocalizationErrors.LanguageNativeNameRequired;

        NativeName = nativeName.Trim();

        return Result.Success();
    }

    #endregion
}