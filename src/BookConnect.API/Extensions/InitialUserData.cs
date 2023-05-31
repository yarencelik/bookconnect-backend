using BookConnect.Application.Features.Auth;
using BookConnect.Application.Features.Shelves;
using BookConnect.Domain.Entities;
using BookConnect.Domain.Enums;
using Serilog;

namespace BookConnect.Infrastructure.Persistence.Seeds;

public static class InitialUserData
{
    public static WebApplication SeedUserData(this WebApplication app, IConfiguration config, IPasswordService passwordService, IShelfService shelfService)
    {
        Log.Information("Checking User Data...");
        using (var scope = app.Services.CreateScope())
        {
            using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                try
                {
                    context.Database.EnsureCreated();
                    var data = context.Users.FirstOrDefault();
                    if (data == null)
                    {
                        Log.Information("User Table is empty. Will proceed to Add Data...");
                        var users = new List<User>
                        {

                            new User
                            {
                                Id = Guid.NewGuid(),
                                Username = config["Seed:Admin_Username"]!,
                                Password = passwordService.HashPassword(config["Seed:Admin_Password"]!),
                                Email = config["Seed:Admin_Email"]!,
                                Role = UserRole.Admin,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                            },
                            new User
                            {
                                Id = Guid.NewGuid(),
                                Username = config["Seed:Reader_Username"]!,
                                Password = passwordService.HashPassword(config["Seed:Reader_Password"]!),
                                Email = config["Seed:Reader_Email"]!,
                                Role = UserRole.Reader,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                            }
                        };

                        context.Users.AddRange(users);
                        context.SaveChanges();

                        var shelvesForAdmin = shelfService.GenerateShelves(users[0].Id);
                        var shelvesForReader = shelfService.GenerateShelves(users[1].Id);

                        context.Shelves.AddRange(shelvesForAdmin);
                        context.Shelves.AddRange(shelvesForReader);
                        context.SaveChanges();

                        Log.Information("Added User Inital Data.");
                    }
                    else {
                        Log.Information("Users already exists.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        return app;
    }
}