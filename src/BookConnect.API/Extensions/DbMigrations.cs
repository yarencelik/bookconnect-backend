using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace App.API.Extensions;

public static class DbMigrations
{
    public static async Task<WebApplication> AddMigrations(this WebApplication webApplication)
    {

        using (var scope = webApplication.Services.CreateScope())
        {
            Log.Information("Checking Migrations...");
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                Log.Information("Migrating Changes to the Database...");
                await context.Database.MigrateAsync();
                Log.Information("Migration Complete.");
            }
            else
                Log.Information("No Pending Migrations.");
        }
        return webApplication;
    }
}
