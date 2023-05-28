using System.Linq.Expressions;
using App.Application.Features.Author.Models;
using AuthorEntity = App.Domain.Entities.Author;
using AutoMapper;
using MediatR;
using App.Application.Common.Exceptions;

namespace App.Application.Features.Author.Queries.GetAuthorById;

sealed class GetAuthorsByIdQueryHandler : IRequestHandler<GetAuthorsByIdQuery, AuthorDetailsDto>
{
    private readonly IAuthorsRepository _authorsRepository;
    private readonly IMapper _mapper;
    public GetAuthorsByIdQueryHandler(IAuthorsRepository authorsRepository, IMapper mapper)
    {
        _authorsRepository = authorsRepository;
        _mapper = mapper;
    }
    public async Task<AuthorDetailsDto> Handle(GetAuthorsByIdQuery request, CancellationToken cancellationToken)
    {

        var author = await _authorsRepository.GetValue(
            x => x.Id.ToString() == request.authorId,
            new List<Expression<Func<AuthorEntity, object>>>
            {
                x => x.User!,
                x => x.Books
            }
            )
            ?? throw new NotFoundException($"Author with ID '{request.authorId}' was not found.");

        var mappedAuthor = _mapper.Map<AuthorDetailsDto>(author);

        return mappedAuthor;
    }
}