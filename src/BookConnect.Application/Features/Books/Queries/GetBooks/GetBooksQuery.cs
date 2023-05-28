using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Books.Models;

namespace BookConnect.Application.Features.Books.Queries.GetBooks;

public class GetBooksQuery : BaseQuery<BookDetailsDto>
{
    public required string Search { get; set; }
}

