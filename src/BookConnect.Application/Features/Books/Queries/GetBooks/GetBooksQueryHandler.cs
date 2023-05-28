using System.Linq.Expressions;
using AutoMapper;
using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Books.Models;
using BookConnect.Domain.Entities;
using MediatR;

namespace BookConnect.Application.Features.Books.Queries.GetBooks;

sealed class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PaginatedResults<BookDetailsDto>>
{
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;
    public GetBooksQueryHandler(IBooksRepository booksRepository, IMapper mapper)
    {
        _booksRepository = booksRepository;
        _mapper = mapper;
    }
    public async Task<PaginatedResults<BookDetailsDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var(results, pageData) = await _booksRepository
            .GetAllValuesPaginated(
                request.Page, 
                request.PageSize,
                x => x.Title.ToLower().Contains(request.Search.ToLower()) ||
                x.ISBN.ToLower().Contains(request.Search.ToLower()) ||
                x.Author!.AuthorName!.ToLower().Contains(request.Search.ToLower()),
                new List<Expression<Func<Book, object>>>
                {
                    x => x.Author!
                });

        var mappedResults = _mapper.Map<IEnumerable<BookDetailsDto>>(results);

        var paginatedResults = new PaginatedResults<BookDetailsDto>(mappedResults, pageData);

        return paginatedResults;

    }
}
