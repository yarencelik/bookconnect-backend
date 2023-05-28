using BookConnect.Application.Common.Exceptions;
using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.DeleteShelfById;

public class DeleteShelfCommandHandler : IRequestHandler<DeleteShelfByIdCommand>
{
    private readonly IShelfRepository _shelfRepository;
    public DeleteShelfCommandHandler(IShelfRepository shelfRepository)
    {
        _shelfRepository = shelfRepository; 
    }
    public async Task Handle(DeleteShelfByIdCommand request, CancellationToken cancellationToken)
    {
        var shelf = await _shelfRepository.GetValue(x => x.Id.ToString() == request.ShelfId, null, false)
            ?? throw new NotFoundException($"Shelf with Id '{request.ShelfId}' was not found");

        _shelfRepository.Delete(shelf);
        await _shelfRepository.SaveChangesAsync();
    }
}