using MediatR;

namespace BookConnect.Application.Features.Books.Commands.AddBook;

public class AddBookCommand : IRequest<string>
{
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public int Pages { get; set; }
    public required string Genre { get; set; }
    public string? AuthorName { get; set; }
}

