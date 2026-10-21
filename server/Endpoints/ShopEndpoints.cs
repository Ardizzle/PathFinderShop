using System.Security.Claims;
using ShopApp.Server.Auth;
using ShopApp.Server.Data;
using ShopApp.Server.Models;

namespace ShopApp.Server.Endpoints;

public static class ShopEndpoints
{
    private const int MaxShopNameLength = 100;

    public static void MapShopEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/shops").RequireAuthorization();

        group.MapPost("/", CreateShop);
        group.MapGet("/{shopId:guid}", GetShop);
    }

    private static async Task<IResult> CreateShop(CreateShopRequest request, ClaimsPrincipal user, ShopRepo shops)
    {
        Guid? appUserId = user.GetAppUserId();

        if(appUserId is null)
        {
            return Results.Unauthorized();
        }

        string shopName = request.ShopName?.Trim() ?? string.Empty;

        if(shopName.Length == 0)
        {
            return Results.BadRequest(new {error = "Shop name is required."});
        }

        if(shopName.Length > MaxShopNameLength)
        {
            return Results.BadRequest(new {error = $"Shop name must be {MaxShopNameLength} characters or fewer."});
        }

        Guid shopId = await shops.CreateAsync(appUserId.Value, shopName);

        return Results.Created($"/shops/{shopId}", new ShopDto(shopId, shopName));
    }

    private static async Task<IResult> GetShop(Guid shopId, ClaimsPrincipal user, ShopRepo shops)
    {
        Guid? appUserId = user.GetAppUserId();

        if(appUserId is null)
        {
            return Results.Unauthorized();
        }

        ShopDto? shop = await shops.GetShopByIdForUser(shopId, appUserId.Value);

        return shop is null ? Results.NotFound() : Results.Ok(shop);
    }
}