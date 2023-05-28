using MediatR;

namespace App.Application.Features.Books.Commands.DeleteBookById;

public record DeleteBookCommand(string bookId) : IRequest;