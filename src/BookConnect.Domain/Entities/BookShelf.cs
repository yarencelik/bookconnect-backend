using BookConnect.Domain.Common;

namespace BookConnect.Domain.Entities;

public class BookShelf : BaseEntity
{
    public Guid BookId { get; set; } 
    public Book? Book { get; set; }
    public Guid ShelfId { get; set; }
    public Shelf? Shelf { get; set; }
}