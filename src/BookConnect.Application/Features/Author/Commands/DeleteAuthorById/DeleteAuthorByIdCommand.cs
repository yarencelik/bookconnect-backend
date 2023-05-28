using MediatR;

namespace BookConnect.Application.Features.Author.Commands.DeleteAuthorById;

public record DeleteAuthorByIdCommand(string authorId) : IRequest;