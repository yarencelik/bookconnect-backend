using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.CreateShelf;

public class CreateShelfCommand : IRequest<Guid>
{
    public required string ShelfName { get; set; } 
}