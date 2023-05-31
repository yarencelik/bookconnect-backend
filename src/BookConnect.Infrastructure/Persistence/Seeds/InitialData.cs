using BookConnect.Application.Features.Auth;
using BookConnect.Application.Features.Shelves;
using BookConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookConnect.Infrastructure.Persistence.Seeds;

public static class InitialData
{
    public static ModelBuilder AddInitialData(this ModelBuilder modelBuilder, IConfiguration config, IPasswordService passwordService, IShelfService shelfService)
    {

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