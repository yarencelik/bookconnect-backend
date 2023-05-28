using BookConnect.Application.Features.Shelves;
using BookConnect.Domain.Entities;

namespace BookConnect.Infrastructure.Services;

public sealed class ShelfService : IShelfService
{
    // Will Generate/Create and Return the default Bookshelves
    public IEnumerable<Shelf> GenerateShelves(Guid userId)
    {
        return new List<Shelf>
        {
            new Shelf()
            {
                Id = Guid.NewGuid(),
                ShelfName = "Favorite",
                OwnerId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Shelf()
            {
                Id = Guid.NewGuid(),
                ShelfName = "Currently Reading",
                OwnerId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Shelf()
            {
                Id = Guid.NewGuid(),
                ShelfName = "Read",
                OwnerId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}