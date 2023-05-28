using BookConnect.Domain.Common;

namespace BookConnect.Domain.Entities;

public class Shelf : BaseEntity
{
    public required string ShelfName { get; set; }   

    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }

    public ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();
}