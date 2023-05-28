using App.Application.Common.Models;
using App.Application.Features.Author.Models;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Author.Queries.GetAuthors;

sealed class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, PaginatedResults<AuthorDetailsDto>>
{
    private readonly IAuthorsRepository _authorsRepository;
    private readonly IMapper _mapper;
    public GetAuthorsQueryHandler(IAuthorsRepository authorsRepository, IMapper mapper)
    {
        _authorsRepository = authorsRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResults<AuthorDetailsDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var (results, pageData) = await _authorsRepository.GetAllValuesPaginated(
            request.Page, 
            request.PageSize,
            x => x.AuthorName!.ToLower().Contains(request.Name.ToLower())); 

        var mappedAuthors = _mapper.Map<IEnumerable<AuthorDetailsDto>>(results);

        return new PaginatedResults<AuthorDetailsDto>(mappedAuthors, pageData);
    }
}