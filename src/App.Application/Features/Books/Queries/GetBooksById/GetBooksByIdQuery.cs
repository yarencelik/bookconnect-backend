using App.Application.Features.Books.Models;
using MediatR;

namespace App.Application.Features.Books.Queries.GetBooksById;

public record GetBooksByIdQuery(string BooksId) : IRequest<BookDetailsDto>;