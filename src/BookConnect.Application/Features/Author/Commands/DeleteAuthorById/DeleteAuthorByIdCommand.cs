using MediatR;

namespace App.Application.Features.Author.Commands.DeleteAuthorById;

public record DeleteAuthorByIdCommand(string authorId) : IRequest;