using BookConnect.Application.Features.Auth;
using BookConnect.Domain.Entities;
using BookConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookConnect.Infrastructure.Persistence.Seeds;

public static class InitialData
{
public static ModelBuilder AddInitialData(this ModelBuilder modelBuilder, IConfiguration config, IPasswordService passwordService) 
{

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

    modelBuilder
        .Entity<User>()
        .HasData(users);

    var book = new List<Book>
    {
        new Book
        {
            Id = Guid.NewGuid(),
            Title = "SampleTitle",
            ISBN = "9781234567897",
            Pages = 300,
            Genre = "SampleGenre",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        },
        new Book
        {
            Id = Guid.NewGuid(),
            Title = "SampleTitle2",
            ISBN = "9781234567444",
            Pages = 200,
            Genre = "SampleGenre2",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        }
    };

    modelBuilder
        .Entity<Book>()
        .HasData(book);
     
    return modelBuilder;
}
}