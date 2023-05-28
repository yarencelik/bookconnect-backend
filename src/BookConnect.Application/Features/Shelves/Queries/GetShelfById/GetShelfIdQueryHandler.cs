using System.Linq.Expressions;
using AutoMapper;
using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Shelves.Models;
using BookConnect.Domain.Entities;
using MediatR;

namespace BookConnect.Application.Features.Shelves.Queries.GetShelfById;

public class GetShelfIdQueryHandler : IRequestHandler<GetShelfByIdQuery, PaginatedResults<ShelfDetailsDto>>
{
    private readonly IBookShelfRepository _bookShelfRepository;
    private readonly IMapper _mapper;
    public GetShelfIdQueryHandler(IBookShelfRepository bookShelfRepository, IMapper mapper)
    {
        _bookShelfRepository = bookShelfRepository;
        _mapper = mapper; 
    }
    public async Task<PaginatedResults<ShelfDetailsDto>> Handle(GetShelfByIdQuery request, CancellationToken cancellationToken)
    {
        var (results, pageData) = await _bookShelfRepository.GetAllValuesPaginated(
            request.Page,
            request.PageSize,
            x => x.ShelfId.ToString() == request.ShelfId,
            new List<Expression<Func<BookShelf, object>>>
            {
                x => x.Book!,
                x => x.Shelf!
            }
        );

        var mappedResults = _mapper.Map<IEnumerable<ShelfDetailsDto>>(results);

        return new PaginatedResults<ShelfDetailsDto>(mappedResults, pageData);
    }
}