namespace BookConnect.Application.Features.Books.Models;

public class BookDetailsDto
{
    public Guid Id { get; set; }
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public int Pages { get; set; }
    public required string Genre { get; set; }
    public string? AuthorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
