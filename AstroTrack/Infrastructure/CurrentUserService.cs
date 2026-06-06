using System.Security.Claims;

namespace AstroTrack.Infrastructure;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor)
{
    public string? GetCurrentUserEmail()
    {
        return httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.Email);
    }
}