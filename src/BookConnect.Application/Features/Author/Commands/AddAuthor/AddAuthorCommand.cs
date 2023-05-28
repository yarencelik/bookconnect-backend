using MediatR;

namespace BookConnect.Application.Features.Author.Commands.AddAuthor;

public record AddAuthorCommand(string AuthorName) : IRequest<Guid>;