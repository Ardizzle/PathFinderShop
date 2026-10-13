using ShopApp.Server.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Dapper;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(options =>
{
  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;  
})
.AddCookie()
.AddGoogle(options =>
{
   options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
   options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!; 
});
builder.Services.AddAuthorization();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

DatabaseMigrator.Run(connectionString);

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login/google", ()=>Results.Challenge(new AuthenticationProperties { RedirectUri = "/login/google/callback"}, new[] { GoogleDefaults.AuthenticationScheme}));

app.MapGet("/login/google/callback", async (HttpContext context, IDbConnection db) =>
{
    AuthenticateResult result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    
    var googleId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var name = context.User.FindFirst(ClaimTypes.Name)?.Value ?? "New User";

    if(googleId is null)
    {
        return Results.Unauthorized();
    }

    Guid appUserId;

    string lookForUserSql = "SELECT AppUserId FROM UserLogin WHERE Provider = 'Google' AND ProviderUserId = @googleId";
    var existingId = await db.QuerySingleOrDefaultAsync<Guid?>(lookForUserSql, new {googleId});

    if(existingId is null)
    {
        Console.WriteLine(name);
        string insertAppUserSql = "INSERT INTO AppUser (DisplayName) OUTPUT INSERTED.AppUserId VALUES (@name)";
        appUserId = await db.QuerySingleAsync<Guid>(insertAppUserSql, new {name});

        string insertUserLoginSql = "INSERT INTO UserLogin (AppUserId, Provider, ProviderUserId) VALUES (@appUserId, 'Google', @googleId)";
        await db.ExecuteAsync(insertUserLoginSql, new {appUserId, googleId});
    }
    else
    {
        appUserId = existingId.Value;
    }

    List<Claim> claims = new List<Claim>
    {
        new Claim("app_user_id", appUserId.ToString()),
        new Claim(ClaimTypes.Name, name)
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

    return Results.Ok(new { appUserId });
});

app.MapGet("/me", (ClaimsPrincipal user) =>
{
    string? id = user.FindFirst("app_user_id")?.Value;
    string? name = user.FindFirst(ClaimTypes.Name)?.Value;
    return Results.Ok(new {appUserId = id, displayName = name});
}).RequireAuthorization();

app.Run();
