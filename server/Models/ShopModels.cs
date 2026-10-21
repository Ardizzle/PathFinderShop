namespace ShopApp.Server.Models;

public record CreateShopRequest(string? ShopName);

public record ShopDto(Guid ShopID, string ShopName);