

using Ecommerce.Domain.Domain;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Errors
{
    public static class MoneyErrors
    {
        public static Error NegativeAmount =>
            new Error("Negative.Amount", "Amount can not be nagative", ErrorType.Validation);
        public static Error InvalidCurrency =>
            new Error("Invalid.Currency", "The currency is invalid", ErrorType.Validation);
        public static Error CurrencyMismatch=>
            new Error("Currency.Mismatch", "Currency is Mismatch",ErrorType.Validation);
        public static Error EnsureSameCurrency =>
            new Error("Ensure.Same.Currency", "Ensure is Same Currency", ErrorType.Validation);
        public static Error QuantityMustBePositive =>
            new Error("Quantity.MustBePositive", "Quantity must be positive", ErrorType.Validation);


    }
}
