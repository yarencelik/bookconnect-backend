using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.DeleteShelfById;

public record DeleteShelfByIdCommand(string ShelfId) : IRequest;