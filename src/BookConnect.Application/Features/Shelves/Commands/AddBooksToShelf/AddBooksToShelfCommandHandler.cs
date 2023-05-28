using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Features.Books;
using BookConnect.Domain.Entities;
using MediatR;

namespace BookConnect.Application.Features.Shelves.Commands.AddBooksToShelf;

sealed class AddBooksToShelfCommandHandler : IRequestHandler<AddBooksToShelfCommand, Guid>
{
    private readonly IBookShelfRepository _bookShelfRepository;
    private readonly IBooksRepository _booksRepository;
    private readonly IShelfRepository _shelfRepository;
    public AddBooksToShelfCommandHandler(IBookShelfRepository bookShelfRepository, IBooksRepository booksRepository, IShelfRepository shelfRepository)
    {
        _bookShelfRepository = bookShelfRepository;
        _booksRepository = booksRepository;
        _shelfRepository = shelfRepository; 
    }
    public async Task<Guid> Handle(AddBooksToShelfCommand request, CancellationToken cancellationToken)
    {
        var existingBook = await _bookShelfRepository.GetValue(x => x.BookId.ToString() == request.BookId &&
        x.ShelfId.ToString() == request.ShelfId);

        if(existingBook != null)
            throw new ConflictException($"Book with Id '{request.BookId}' already exists on the Requested Shelf with Id '{request.ShelfId}'");

        var book = await _booksRepository.GetValue(x => x.Id.ToString() == request.BookId)
            ?? throw new NotFoundException($"Book with Id '{request.BookId}' was not found.");

        var shelf = await _shelfRepository.GetValue(x => x.Id.ToString() == request.ShelfId)
            ?? throw new NotFoundException($"Shelf with Id '{request.ShelfId}' was not found.");


        BookShelf newBookShelf = new()
        {
            BookId = book.Id,
            ShelfId = shelf.Id
        };

        await _bookShelfRepository.Create(newBookShelf);
        await _bookShelfRepository.SaveChangesAsync();

        return newBookShelf.Id;
    }
}