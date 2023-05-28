using MediatR;

namespace App.Application.Features.Author.Commands.AddAuthor;

public record AddAuthorCommand(string AuthorName) : IRequest<Guid>;