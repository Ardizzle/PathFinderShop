using System.Reflection;
using DbUp;
using DbUp.Engine;

namespace ShopApp.Server.Database;

public static class DatabaseMigrator
{
    public static void Run(string connectionString)
    {
        UpgradeEngine upgrader = DeployChanges.To.SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        DatabaseUpgradeResult result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
            throw new Exception("Database migration failed. See console output for details.");
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Database migration completed successfully.");
        Console.ResetColor();
    }
}