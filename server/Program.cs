using System.Data;
using Microsoft.Data.SqlClient;
using ShopApp.Server.Auth;
using ShopApp.Server.Data;
using ShopApp.Server.Database;
using ShopApp.Server.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ShopRepo>();
builder.Services.AddAppAuthentication(builder.Configuration);

WebApplication app = builder.Build();

DatabaseMigrator.Run(connectionString);

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();
app.MapShopEndpoints();

app.Run();
