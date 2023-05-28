namespace BookConnect.Application.Features.Shelves.Models;

public class ShelvesDetailsDto
{
    public Guid Id { get; set; }
    public required string ShelfName { get; set; }   
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}