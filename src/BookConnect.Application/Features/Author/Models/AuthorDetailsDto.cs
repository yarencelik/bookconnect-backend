using BookConnect.Domain.Entities;

namespace BookConnect.Application.Features.Author.Models;

public class AuthorDetailsDto
{
    public Guid Id { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public User? User { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}