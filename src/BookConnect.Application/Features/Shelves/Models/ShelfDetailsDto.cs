using BookConnect.Application.Features.Books.Models;

namespace BookConnect.Application.Features.Shelves.Models;

public class ShelfDetailsDto
{
    public Guid BookId { get; set; } 
    public BookDetailsDto? Book { get; set; }
    public Guid ShelfId { get; set; }
    public ShelvesDetailsDto? Shelf { get; set; }
}