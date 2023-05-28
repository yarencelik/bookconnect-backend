using App.Application.Common.Exceptions;
using MediatR;
using AuthorEntity = App.Domain.Entities.Author;

namespace App.Application.Features.Author.Commands.AddAuthor;

sealed class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Guid>
{
    private readonly IAuthorsRepository _authorsRepository;
    public AddAuthorCommandHandler(IAuthorsRepository authorsRepository)
    {
        _authorsRepository = authorsRepository;
    }
    public async Task<Guid> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _authorsRepository.GetValue(x => x.AuthorName!.ToLower() == request.AuthorName.ToLower());
        if(existingAuthor != null)
        {
            throw new ConflictException($"Author with a Name '{request.AuthorName}' already exists.");
        }

        var newAuthor = new AuthorEntity
        {
            AuthorName = request.AuthorName
        };

        await _authorsRepository.Create(newAuthor);
        await _authorsRepository.SaveChangesAsync();

        return newAuthor.Id;
    }
}
