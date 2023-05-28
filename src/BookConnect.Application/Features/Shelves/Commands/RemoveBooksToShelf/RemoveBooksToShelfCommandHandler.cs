using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Features.Books;
using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.RemoveBooksToShelf;

sealed class RemoveBooksToShelfCommandHandler : IRequestHandler<RemoveBooksToShelfCommand>
{
    private readonly IBookShelfRepository _bookShelfRepository;
    private readonly IBooksRepository _booksRepository;
    private readonly IShelfRepository _shelfRepository;
    public RemoveBooksToShelfCommandHandler(IBookShelfRepository bookShelfRepository, IBooksRepository booksRepository, IShelfRepository shelfRepository)
    {
        _bookShelfRepository = bookShelfRepository;
        _booksRepository = booksRepository;
        _shelfRepository = shelfRepository; 
    }
    public async Task Handle(RemoveBooksToShelfCommand request, CancellationToken cancellationToken)
    {
        var shelf = await _shelfRepository.GetValue(x => x.Id.ToString() == request.ShelfId)
            ?? throw new NotFoundException($"Shelf with Id '{request.ShelfId}' was not found.");

        var existingBook = await _bookShelfRepository.GetValue(x => x.BookId.ToString() == request.BookId &&
        x.ShelfId.ToString() == request.ShelfId) ??
            throw new NotFoundException($"Book with Id; '{request.BookId}' was not found on the Shelf with Id '${request.ShelfId}'.");

        _bookShelfRepository.Delete(existingBook);
        await _bookShelfRepository.SaveChangesAsync();
    }
}