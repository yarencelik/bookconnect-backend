using BookConnect.Application.Common.Exceptions;
using MediatR;

namespace BookConnect.Application.Features.Author.Commands.DeleteAuthorById;

sealed class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand>
{
    private readonly IAuthorsRepository _authorsRepository;
    public DeleteAuthorByIdCommandHandler(IAuthorsRepository authorsRepository)
    {
        _authorsRepository = authorsRepository;
    }
    public async Task Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorsRepository.GetValue(x => x.Id.ToString() == request.authorId)
            ?? throw new NotFoundException($"Author with Id '{request.authorId}' was not found.");

        _authorsRepository.Delete(author);
        await _authorsRepository.SaveChangesAsync(); 
    }
}
