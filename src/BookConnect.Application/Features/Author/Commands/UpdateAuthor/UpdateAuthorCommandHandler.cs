using AutoMapper;
using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Features.Author.Models;
using MediatR;

namespace BookConnect.Application.Features.Author.Commands.UpdateAuthor;

sealed class UpdateAuthorsCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IAuthorsRepository _authorsRepository;
    private readonly IMapper _mapper;
    public UpdateAuthorsCommandHandler(IAuthorsRepository authorsRepository, IMapper mapper)
    {
        _authorsRepository = authorsRepository;
        _mapper = mapper;
    }
    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorsRepository.GetValue(x => x.Id.ToString() == request.authorId, AsNoTracking: false)
            ?? throw new NotFoundException($"Author with Id: {request.authorId} was not found.");

        var authorToUpdate = _mapper.Map<UpdateAuthorDto>(author);

        request.UpdateAuthorDto.ApplyTo(authorToUpdate, x => throw new Exception("Error in JsonPatchDocument"));

        // TODO: Add a Logic that app should check if the entered username exists
        /* if Username prop is not null/(!string.IsNullOrEmpty), proceed with the check if username already exist
         * and assign to the Author
         * if Username prop is null/empty, leave it as is
         * Note: This goes with the 'Book' entity too.
        */

        var existingAuthor = await _authorsRepository.GetValue(x => x.AuthorName!.ToLower() == authorToUpdate.AuthorName.ToLower(), null, false);
        if(existingAuthor != null)
        {
            throw new ConflictException($"Author with a Name '{authorToUpdate.AuthorName}' already exists.");
        }

        var updated = _mapper.Map(authorToUpdate, author);
        await _authorsRepository.SaveChangesAsync();
    }
}

