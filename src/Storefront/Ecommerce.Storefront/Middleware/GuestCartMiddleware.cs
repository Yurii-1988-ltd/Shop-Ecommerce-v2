namespace Ecommerce.Storefront.Middleware;

public sealed class GuestCartMiddleware(RequestDelegate next)
{
    public const string CookieName = "Ecommerce.GuestId";

    public async Task InvokeAsync(HttpContext context)
    {
        if(!context.User.Identity?.IsAuthenticated==true && !context.Request.Cookies.ContainsKey(CookieName))
        {
            context.Response.Cookies.Append(CookieName,
                Guid.NewGuid().ToString(),
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    IsEssential = true,
                });
        }
        await next(context);
    }


}
