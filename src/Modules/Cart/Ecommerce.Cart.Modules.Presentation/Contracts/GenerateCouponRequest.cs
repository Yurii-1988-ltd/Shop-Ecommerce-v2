// DTO без поля Code
public record GenerateCouponRequest(
    string Type,
    decimal AmountOrPercentage,
    decimal MinimumSpend,
    string Currency,
    DateTime ExpirationDateUtc,
    decimal? MaxDiscountAmount = null);