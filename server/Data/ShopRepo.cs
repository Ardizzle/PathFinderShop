using System.Data;
using Dapper;
using ShopApp.Server.Models;

namespace ShopApp.Server.Data;

public class ShopRepo
{
    private readonly IDbConnection db;

    public ShopRepo(IDbConnection db)
    {
        this.db = db;
    }

    public async Task<Guid> CreateAsync(Guid appUserId, string shopName)
    {
        string sql = "INSERT INTO Shop (AppUserId, ShopName) OUTPUT INSERTED.ShopId VALUES(@appUserId, @ShopName)";

        return await this.db.QuerySingleAsync<Guid>(sql, new { appUserId, shopName});
    }

    public async Task<ShopDto?> GetShopByIdForUser(Guid shopId, Guid appUserId)
    {
        string sql = "SELECT ShopId, ShopName FROM Shop WHERE AppUserId = @appUserId AND ShopId = @shopId";

        return await this.db.QuerySingleOrDefaultAsync<ShopDto>(sql, new {shopId, appUserId});
    }
}