using System.Security.Claims;

namespace ShopApp.Server.Auth;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetAppUserId(this ClaimsPrincipal user)
    {
        return Guid.TryParse(user.FindFirst(AppClaimTypes.AppUserId)?.Value, out Guid appUserId) ? appUserId : null;
    }
}