using AutoMapper;
using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Features.Books.Models;
using MediatR;

namespace BookConnect.Application.Features.Books.Queries.GetBooksById;

sealed class GetBooksByIdQueryHandler : IRequestHandler<GetBooksByIdQuery, BookDetailsDto>
{
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;

    public GetBooksByIdQueryHandler(IBooksRepository booksRepository, IMapper mapper)
    {
        _booksRepository = booksRepository;
        _mapper = mapper;
    }

    public async Task<BookDetailsDto> Handle(GetBooksByIdQuery request, CancellationToken cancellationToken) 
    {
       var book = await _booksRepository.GetValue(x => x.Id.ToString() == request.BooksId);

       if(book == null)
       {
            throw new NotFoundException($"Book with the ID '{request.BooksId}' was not found.");
       }

        var mappedBookDetails = _mapper.Map<BookDetailsDto>(book);

        return mappedBookDetails;
    }
}