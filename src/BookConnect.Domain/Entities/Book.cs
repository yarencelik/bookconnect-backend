using BookConnect.Domain.Common;

namespace BookConnect.Domain.Entities;

public class Book : BaseEntity
{
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public int Pages { get; set; }
    public required string Genre { get; set; }

    public Guid? AuthorId { get; set; }
    public Author? Author { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}