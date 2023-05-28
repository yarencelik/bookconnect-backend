using BookConnect.Application.Common.Exceptions;
using MediatR;

namespace BookConnect.Application.Features.Books.Commands.DeleteBookById;

sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBooksRepository _booRepository;

    public DeleteBookCommandHandler(IBooksRepository booRepository)
    {
        _booRepository = booRepository;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _booRepository.GetValue(x => x.Id.ToString() == request.bookId)
            ?? throw new NotFoundException($"Book with ID '{request.bookId}' was not found.");

        _booRepository.Delete(book);
        await _booRepository.SaveChangesAsync();
    }
}
