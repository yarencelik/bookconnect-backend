using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.RemoveBooksToShelf;

public record RemoveBooksToShelfCommand(string ShelfId, string BookId) : IRequest;