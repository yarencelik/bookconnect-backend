using App.Application.Common.Models;
using App.Application.Features.Books.Models;

public class GetBooksQuery : BaseQuery<BookDetailsDto>
{
    public required string Search { get; set; }
}

