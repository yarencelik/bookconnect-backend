using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.AddBooksToShelf;

public record AddBooksToShelfCommand(string ShelfId, string BookId) : IRequest<Guid>;