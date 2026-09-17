namespace Ecommerce.Storefront.ApiClients.Carts.Services;

public class GuestCartService(IHttpContextAccessor httpContextAccessor) : IGuestCartService
{
    private const string CookieName = "Ecommerce.GuestId";
    public Guid GetGuestId()
    {
       var httpContext = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is not available.");
        if(httpContext.Request.Cookies.TryGetValue(CookieName,out var value)&&
            Guid.TryParse(value,out var guestId)&&
            guestId!=Guid.Empty)
        {
            return guestId;
        }
        guestId = Guid.NewGuid();
        httpContext.Response.Cookies.Append(
            CookieName,
            guestId.ToString(),
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                IsEssential = true

            });
        return guestId;

    }
}
