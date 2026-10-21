using System.Data;
using Dapper;

namespace ShopApp.Server.Data;

public class UserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<Guid?> FindUserIdByLoginAsync(string provider, string providerUserId)
    {
        string sql = "SELECT AppUserId FROM UserLogin WHERE Provider = @provider AND ProviderUserId = @providerUserId";

        return await _db.QuerySingleOrDefaultAsync<Guid?>(sql, new {provider, providerUserId});
    }

    public async Task<Guid> CreateUserWithLoginAsync(string displayName, string provider, string providerUserId)
    {
        string insertAppUserSql = "INSERT INTO AppUser (DisplayName) OUTPUT INSERTED.AppUserId VALUES (@displayName)";
        string insertUserLoginSql = "INSERT INTO UserLogin (AppUserId, Provider, ProviderUserId) VALUES (@appUserId, @provider, @providerUserId)";

        if(_db.State != ConnectionState.Open)
        {
            _db.Open();
        }

        using IDbTransaction transaction = _db.BeginTransaction();

        Guid appUserId = await _db.QuerySingleAsync<Guid>(insertAppUserSql, new {displayName}, transaction);
        await _db.ExecuteAsync(insertUserLoginSql, new {appUserId, provider, providerUserId}, transaction);

        transaction.Commit();

        return appUserId;
    }
}