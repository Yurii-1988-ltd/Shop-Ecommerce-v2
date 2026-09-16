internal sealed class CurrentUser(IHttpContextAccessor httpContextAccessor)
    : ICurrentUser
{
    private ClaimsPrincipal User =>
        httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();

    public Guid UserId
    {
        get
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(value, out var userId)
                ? userId
                : Guid.Empty;
        }
    }

    public string? Email =>
        User.FindFirstValue(ClaimTypes.Email);

    public bool IsAuthenticated =>
        User.Identity?.IsAuthenticated == true;

    public bool IsInRole(string role) =>
        User.IsInRole(role);
}