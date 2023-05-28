using MediatR;

namespace BookConnect.Application.Features.Books.Commands.DeleteBookById;

public record DeleteBookCommand(string bookId) : IRequest;