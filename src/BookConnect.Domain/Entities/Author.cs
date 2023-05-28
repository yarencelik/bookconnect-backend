using BookConnect.Domain.Common;

namespace BookConnect.Domain.Entities;

public sealed class Author : BaseEntity
{
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public string? AuthorName { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}