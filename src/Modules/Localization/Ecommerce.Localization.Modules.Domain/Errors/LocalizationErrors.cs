using Ecommerce.Domain.Domain;

public static class LocalizationErrors
{
    public static readonly Error CultureCodeRequired =
        Error.Validation(
            "Localization.CultureCodeRequired",
            "Culture code is required.");
    public static readonly Error ModuleIsRequired =
        Error.Validation(
            "Localization.ModuleIsRequired",
            "Module is Required is required.");


    public static readonly Error LanguageNameRequired =
        Error.Validation(
            "Localization.LanguageNameRequired",
            "Language name is required.");

    public static readonly Error LanguageNativeNameRequired =
        Error.Validation(
            "Localization.LanguageNativeNameRequired",
            "Language native name is required.");

    public static readonly Error TranslationKeyRequired =
        Error.Validation(
            "Localization.TranslationKeyRequired",
            "Translation key is required.");

    public static readonly Error TranslationValueRequired =
        Error.Validation(
            "Localization.TranslationValueRequired",
            "Translation value is required.");

    public static Error InvalidCultureCode(string cultureCode) =>
        Error.Validation(
            "Localization.InvalidCultureCode",
            $"Culture code '{cultureCode}' is invalid.");

    public static Error TranslationNotFound(
        string key,
        string cultureCode) =>
        Error.NotFound(
            "Localization.TranslationNotFound",
            $"Translation '{key}' for culture '{cultureCode}' was not found.");
    public static Error TranslationNotFound(Guid id) =>
    Error.NotFound(
        "Localization.TranslationNotFound",
        $"Translation with id '{id}' was not found.");

    public static Error DuplicateTranslation(
        string key,
        string cultureCode) =>
        Error.Conflict(
            "Localization.DuplicateTranslation",
            $"Translation '{key}' for culture '{cultureCode}' already exists.");
    public static Error DuplicateLanguage(string cultureCode)
        => Error.Conflict(
            "Localization.DuplicateLanguage",
            $"Language for culture '{cultureCode}' already exists.");
    public static Error DefaultLanguageAlreadyExists
     => Error.Conflict(
         "Localization.DefaultLanguageAlreadyExists",
         "A default language already exists.");
    public static Error LanguageNotFound(Guid id)
        => Error.NotFound(
        "Localization.LanguageNotFound",
        $"Language with id '{id}' was not found.");
    public static Error CannotDeleteDefaultLanguage
    => Error.Conflict(
        "Localization.CannotDeleteDefaultLanguage",
        "The default language cannot be deleted.");
}