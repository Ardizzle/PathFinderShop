using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using ShopApp.Server.Auth;
using ShopApp.Server.Data;

namespace ShopApp.Server.Endpoints;

public static class AuthEndpoints
{
    private const string GoogleProvider = "Google";

    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGet("/login/google", LoginWithGoogle);
        app.MapGet("login/google/callback", GoogleCallback);
        app.MapGet("/me", GetCurrentUser).RequireAuthorization();
    }

    private static IResult LoginWithGoogle()
    {
        AuthenticationProperties properties = new AuthenticationProperties
        {
            RedirectUri = "login/google/callback"
        };

        return Results.Challenge(properties, new[] {GoogleDefaults.AuthenticationScheme});
    }

    private static async Task<IResult> GoogleCallback(HttpContext context, UserRepository userRepo)
    {
        string? googleId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string displayName = context.User.FindFirst(ClaimTypes.Name)?.Value ?? "New User";

        if(googleId is null)
        {
            return Results.Unauthorized();
        }

        Guid appUserId;
        Guid? existingId = await userRepo.FindUserIdByLoginAsync(GoogleProvider, googleId);

        if(existingId is null)
        {
            appUserId = await userRepo.CreateUserWithLoginAsync(displayName, GoogleProvider, googleId);
        }
        else
        {
            appUserId = existingId.Value;
        }

        List<Claim> claims = new List<Claim>
        {
            new Claim(AppClaimTypes.AppUserId, appUserId.ToString()),
            new Claim(ClaimTypes.Name, displayName)
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return Results.Ok(new {appUserId});
    }

    private static IResult GetCurrentUser(ClaimsPrincipal user)
    {
        string? appUserId = user.FindFirst(AppClaimTypes.AppUserId)?.Value;
        string? displayName = user.FindFirst(ClaimTypes.Name)?.Value;

        return Results.Ok(new {appUserId, displayName});
    }
}