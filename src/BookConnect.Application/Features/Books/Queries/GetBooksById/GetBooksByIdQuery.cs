using BookConnect.Application.Features.Books.Models;
using MediatR;

namespace BookConnect.Application.Features.Books.Queries.GetBooksById;

public record GetBooksByIdQuery(string BooksId) : IRequest<BookDetailsDto>;